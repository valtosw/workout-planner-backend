using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WorkoutPlanner.Models.AuthModels;
using WorkoutPlanner.Services;

namespace Tests.UnitTests.Services
{
    public class JwtServiceTests
    {
        private readonly JwtService _jwtService;
        private readonly JwtSettings _jwtSettings;

        public JwtServiceTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                {"Jwt:AccessTokenSecret", "this-is-a-super-secure-access-token-secret-key-for-testing"},
                {"Jwt:RefreshTokenSecret", "this-is-an-even-more-secure-refresh-token-secret-for-testing"},
                {"Jwt:Issuer", "test.issuer.com"},
                {"Jwt:Audience", "test.audience.com"},
                {"Jwt:AccessTokenExpiryInSeconds", "60"},
                {"Jwt:RefreshTokenExpiryInSeconds", "120"}
                })
                .Build();

            _jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>()!;
            _jwtService = new JwtService(configuration);
        }

        private static List<Claim> GetSampleClaims() =>
        [
            new(ClaimTypes.NameIdentifier, "test-user-id"),
            new(ClaimTypes.Email, "test@example.com"),
            new(ClaimTypes.Role, "Customer")
        ];

        [Fact]
        public void GenerateAccessToken_ShouldCreateValidTokenWithCorrectClaims()
        {
            // Arrange
            var claims = GetSampleClaims();

            // Act
            var token = _jwtService.GenerateAccessToken(claims);

            // Assert
            token.Should().NotBeNullOrEmpty();
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(token);

            decodedToken.Issuer.Should().Be(_jwtSettings.Issuer);
            decodedToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Aud).Value.Should().Be(_jwtSettings.Audience);
            decodedToken.Claims.First(c => c.Type == ClaimTypes.Email).Value.Should().Be("test@example.com");
        }

        [Fact]
        public void ValidateAccessToken_WithValidToken_ShouldReturnClaimsPrincipal()
        {
            // Arrange
            var claims = GetSampleClaims();
            var token = _jwtService.GenerateAccessToken(claims);

            // Act
            var principal = _jwtService.ValidateAccessToken(token);

            // Assert
            principal.Should().NotBeNull();
            principal!.Identity!.IsAuthenticated.Should().BeTrue();
            principal.Claims.First(c => c.Type == ClaimTypes.Email).Value.Should().Be("test@example.com");
        }

        [Fact]
        public void ValidateAccessToken_WithInvalidTokenSignature_ShouldReturnNull()
        {
            // Arrange
            var invalidToken = "this.is.an.invalid.token";

            // Act
            var principal = _jwtService.ValidateAccessToken(invalidToken);

            // Assert
            principal.Should().BeNull();
        }

        [Fact]
        public async Task ValidateAccessToken_WithExpiredToken_ShouldReturnNull()
        {
            // Arrange
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                {"Jwt:AccessTokenSecret", "another-secure-secret-for-short-lived-token-testing"},
                {"Jwt:Issuer", "test.issuer.com"},
                {"Jwt:Audience", "test.audience.com"},
                {"Jwt:AccessTokenExpiryInSeconds", "1"}
                })
                .Build();

            var serviceWithShortExpiry = new JwtService(configuration);
            var token = serviceWithShortExpiry.GenerateAccessToken(GetSampleClaims());

            await Task.Delay(1500);

            // Act
            var principal = serviceWithShortExpiry.ValidateAccessToken(token);

            // Assert
            principal.Should().BeNull();
        }
    }
}
