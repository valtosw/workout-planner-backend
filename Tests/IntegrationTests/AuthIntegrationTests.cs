using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using WorkoutPlanner.Data;
using WorkoutPlanner.Models;
using WorkoutPlanner.Models.DTOs.AuthDTOs;

namespace Tests.IntegrationTests
{
    public class AuthIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public AuthIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task FullAuthFlow_RegisterConfirmLogin_ShouldSucceed()
        {
            var email = $"testuser_{Guid.NewGuid()}@example.com";
            var password = "Password123!";
            var registerDto = new RegisterDto
            {
                Email = email,
                Password = password,
                FirstName = "Integration",
                LastName = "Test",
                Role = "Customer"
            };

            var registerResponse = await _client.PostAsJsonAsync("/api/auth/register", registerDto);
            registerResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            string confirmationCode;
            using (var scope = _factory.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var user = await userManager.FindByEmailAsync(email);
                user.Should().NotBeNull();

                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                confirmationCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            }

            var confirmEmailDto = new ConfirmEmailDto { Email = email, Code = confirmationCode };
            var confirmResponse = await _client.PostAsJsonAsync("/api/auth/confirmemail", confirmEmailDto);
            confirmResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var confirmContent = await confirmResponse.Content.ReadFromJsonAsync<AuthResponseDto>();
            confirmContent!.Message.Should().Be("Email confirmed successfully.");

            var loginDto = new LoginDto { Email = email, Password = password };
            var loginResponse = await _client.PostAsJsonAsync("/api/auth/login", loginDto);

            loginResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var loginContent = await loginResponse.Content.ReadFromJsonAsync<LoginResponseDto>();
            loginContent!.AccessToken.Should().NotBeNullOrEmpty();

            loginResponse.Headers.GetValues("Set-Cookie").First().Should().Contain("jwt=");
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ShouldReturnUnauthorized()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "nonexistent@example.com", Password = "wrongpassword" };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/login", loginDto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        private class AuthResponseDto
        {
            public string? Message { get; set; }
        }

        private class LoginResponseDto
        {
            public string? AccessToken { get; set; }
        }
    }
}
