using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using System.Security.Claims;
using System.Text;
using WorkoutPlanner.Data;
using WorkoutPlanner.Models;
using WorkoutPlanner.Models.AuthModels;
using WorkoutPlanner.Models.DTOs.AuthDTOs;
using WorkoutPlanner.Services;

namespace WorkoutPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(AppDbContext context, 
        UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager, 
        JwtService jwtService, 
        EmailService emailService) : ControllerBase
    {
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Wrong register credentials", errors = ModelState });

            if (await userManager.FindByEmailAsync(request.Email) is not null)
                return BadRequest(new { message = "A user with this email already exists." });

            ApplicationUser user;

            if (request.Role.Equals("customer", StringComparison.CurrentCultureIgnoreCase))
            {
                user = new Customer
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    UserName = request.Email,
                };
            }
            else if (request.Role.Equals("trainer", StringComparison.CurrentCultureIgnoreCase))
            {
                user = new Trainer
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    UserName = request.Email,
                };
            }
            else
            {
                return BadRequest(new { message = "Invalid role." });
            }

            var createUserResult = await userManager.CreateAsync(user, request.Password);

            if (user is null)
                return BadRequest(new { message = "User registration failed." });

            await userManager.AddToRoleAsync(user, request.Role);


            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email),
            };

            var roles = await userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            await userManager.AddClaimsAsync(user, claims);

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var confirmationLink = $"http://localhost:5173/confirm-email?email={user.Email}&code={code}";

            await emailService.SendEmailConfirmationAsync(user.Email, confirmationLink);

            return Created(user.Email, new { Message = "Please check your email for a confirmation link." });
        }

        [Route("ConfirmEmail")]
        [HttpPost]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDto request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return BadRequest(new { message = "User not found." });

            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
            var result = await userManager.ConfirmEmailAsync(user, token);

            return result.Succeeded ? 
                Ok(new { Message = "Email confirmed successfully." }) 
                : BadRequest(new { Message = "Email confirmation failed." });
        }

        [Route("CheckEmailConfirmation")]
        [HttpPost]
        public async Task<bool> CheckEmailConfirmation([FromBody] CheckEmailDto request)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(request.Email);

                return user is not null && await userManager.IsEmailConfirmedAsync(user);
            }
            catch (Exception)
            {
                return false;
            }
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Wrong login credentials", errors = ModelState });

            var user = await userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return BadRequest(new { message = "User not found." });

            if (!await userManager.CheckPasswordAsync(user, request.Password))
                return Unauthorized(new { message = "Wrong login or password." });

            if (!await userManager.IsEmailConfirmedAsync(user))
            {
                return Unauthorized(new { message = "Email not confirmed." });
            }

            var claims = userManager.GetClaimsAsync(user).Result.ToList();

            var refreshToken = jwtService.GenerateRefreshToken(claims);
            var accessToken = jwtService.GenerateAccessToken(claims);

            context.RefreshTokens.Add(new RefreshToken { UserId = user.Id, Token = refreshToken });

            await context.SaveChangesAsync();

            Response.Cookies.Append("jwt", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure = true,
                MaxAge = TimeSpan.FromDays(1)
            });

            return Ok(new
            {
                AccessToken = accessToken
            });
        }

        [Route("RefreshAccessToken")]
        [HttpPost]
        public async Task<IActionResult> RefreshAccessToken()
        {
            var refreshToken = Request.Cookies["jwt"];

            var foundToken = await context.RefreshTokens
                .Include(rt => rt.User)
                .SingleOrDefaultAsync(rt => rt.Token == refreshToken);

            if (foundToken is null)
                return Unauthorized(new { Message = "Refresh token is missing or invalid." });

            var user = foundToken.User;

            if (user is null)
                return StatusCode(StatusCodes.Status403Forbidden, new { message = "Forbidden" });

            var claimsPrincipal = jwtService.ValidateRefreshToken(refreshToken);

            if (claimsPrincipal == null || claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value != user.Email)
                return StatusCode(StatusCodes.Status403Forbidden, new { message = "Forbidden" });

            var claims = userManager.GetClaimsAsync(user).Result.ToList();

            var accessToken = jwtService.GenerateAccessToken(claims);

            var newRefreshToken = jwtService.GenerateRefreshToken(claims);

            var existingToken = user.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken);

            if (existingToken != null)
                existingToken.Token = newRefreshToken;

            await context.SaveChangesAsync();

            Response.Cookies.Append("jwt", newRefreshToken, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure = true,
                MaxAge = TimeSpan.FromDays(1)
            });

            return Ok(new { AccessToken = accessToken });
        }

        [Route("Logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["jwt"];

            var user = await context.Users
                .Include(u => u.RefreshTokens)
                .SingleOrDefaultAsync(u => u.RefreshTokens.Any(rt => rt.Token == refreshToken));

            var existingToken = user.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken);

            if (existingToken != null)
            {
                user.RefreshTokens.Remove(existingToken);
                context.RefreshTokens.Remove(existingToken);
            }            

            await context.SaveChangesAsync();

            Response.Cookies.Delete("jwt");

            return Ok(new { Message = "User logged out successfully." });
        }
    }
}
