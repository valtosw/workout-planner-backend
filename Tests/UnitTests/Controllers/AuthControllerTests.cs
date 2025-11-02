using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Security.Claims;
using System.Text;
using WorkoutPlanner.Controllers;
using WorkoutPlanner.Data;
using WorkoutPlanner.Models;
using WorkoutPlanner.Models.DTOs.AuthDTOs;
using WorkoutPlanner.Services;

namespace Tests.UnitTests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private readonly Mock<JwtService> _mockJwtService;
        private readonly Mock<EmailService> _mockEmailService;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    {"Jwt:AccessTokenSecret", "test_secret"},
                    {"Jwt:RefreshTokenSecret", "test_refresh_secret"},
                    {"Jwt:Issuer", "test.com"},
                    {"Jwt:Audience", "test.com"},
                    {"Jwt:AccessTokenExpiryInSeconds", "60"},
                    {"Jwt:RefreshTokenExpiryInSeconds", "120"},
                    {"EmailService:SendGridApiKey", "dummy-api-key-for-testing"},
                    {"EmailService:SenderEmail", "test@test.com"}
                })
                .Build();

            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(
                Mock.Of<IRoleStore<IdentityRole>>(), null, null, null, null);

            var options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;
            var mockDbContext = new Mock<AppDbContext>(options);

            _mockJwtService = new Mock<JwtService>(configuration);
            _mockEmailService = new Mock<EmailService>(configuration);

            _controller = new AuthController(
                mockDbContext.Object,
                _mockUserManager.Object,
                _mockRoleManager.Object,
                _mockJwtService.Object,
                _mockEmailService.Object
            );

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }


        [Fact]
        public async Task Register_WithValidData_ReturnsCreated()
        {
            // Arrange
            var registerDto = new RegisterDto { Email = "new@example.com", Password = "Password123", Role = "Customer", FirstName = "John", LastName = "Doe" };
            _mockUserManager.Setup(um => um.FindByEmailAsync(registerDto.Email)).ReturnsAsync((ApplicationUser)null!);
            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), registerDto.Password)).ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(um => um.GenerateEmailConfirmationTokenAsync(It.IsAny<ApplicationUser>())).ReturnsAsync("confirm-token");
            _mockUserManager.Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(["Customer"]);

            // Act
            var result = await _controller.Register(registerDto);

            // Assert
            var createdResult = result.Should().BeOfType<CreatedResult>().Subject;
            createdResult.Value.Should().BeEquivalentTo(new { Message = "Please check your email for a confirmation link." });
            _mockEmailService.Verify(es => es.SendEmailConfirmationAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Register_WhenEmailExists_ReturnsBadRequest()
        {
            // Arrange
            var registerDto = new RegisterDto { Email = "existing@example.com", Password = "Password123", Role = "Customer", FirstName = "John", LastName = "Doe" };
            var existingUser = new ApplicationUser { FirstName = "Existing", LastName = "User", Email = registerDto.Email, UserName = registerDto.Email };
            _mockUserManager.Setup(um => um.FindByEmailAsync(registerDto.Email)).ReturnsAsync(existingUser);

            // Act
            var result = await _controller.Register(registerDto);

            // Assert
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestResult.Value.Should().BeEquivalentTo(new { message = "A user with this email already exists." });
        }

        [Fact]
        public async Task Login_WithValidCredentialsAndConfirmedEmail_ReturnsOkWithToken()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@example.com", Password = "Password123" };
            var user = new ApplicationUser { Email = loginDto.Email, FirstName = "Test", LastName = "User" };
            var claims = new List<Claim> { new(ClaimTypes.Email, loginDto.Email) };

            _mockUserManager.Setup(um => um.FindByEmailAsync(loginDto.Email)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.CheckPasswordAsync(user, loginDto.Password)).ReturnsAsync(true);
            _mockUserManager.Setup(um => um.IsEmailConfirmedAsync(user)).ReturnsAsync(true);
            _mockUserManager.Setup(um => um.GetClaimsAsync(user)).ReturnsAsync(claims);
            _mockJwtService.Setup(js => js.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>())).Returns("access-token");
            _mockJwtService.Setup(js => js.GenerateRefreshToken(It.IsAny<IEnumerable<Claim>>())).Returns("refresh-token");

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(new { AccessToken = "access-token" });
            _controller.Response.Headers["Set-Cookie"].ToString().Should().Contain("jwt=refresh-token");
        }

        [Fact]
        public async Task Login_WithUnconfirmedEmail_ReturnsUnauthorized()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@example.com", Password = "Password123" };
            var user = new ApplicationUser { Email = loginDto.Email, FirstName = "Test", LastName = "User" };

            _mockUserManager.Setup(um => um.FindByEmailAsync(loginDto.Email)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.CheckPasswordAsync(user, loginDto.Password)).ReturnsAsync(true);
            _mockUserManager.Setup(um => um.IsEmailConfirmedAsync(user)).ReturnsAsync(false);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var unauthorizedResult = result.Should().BeOfType<UnauthorizedObjectResult>().Subject;
            unauthorizedResult.Value.Should().BeEquivalentTo(new { message = "Your email is not confirmed yet." });
        }

        [Fact]
        public async Task ConfirmEmail_WithValidData_ReturnsOk()
        {
            // Arrange
            var user = new ApplicationUser { Email = "test@example.com", FirstName = "Test", LastName = "User" };
            var token = "some-valid-token";
            var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var confirmEmailDto = new ConfirmEmailDto { Email = user.Email, Code = code };

            _mockUserManager.Setup(um => um.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.ConfirmEmailAsync(user, token)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.ConfirmEmail(confirmEmailDto);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(new { Message = "Email confirmed successfully." });
        }
    }
}
