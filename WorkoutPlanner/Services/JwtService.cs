using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkoutPlanner.Models.AuthModels;

namespace WorkoutPlanner.Services
{
    public class JwtService(IConfiguration configuration)
    {
        private readonly JwtSettings _jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();

        public virtual string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            return GenerateJwtToken(claims, _jwtSettings.AccessTokenSecret, TimeSpan.FromSeconds(_jwtSettings.AccessTokenExpiryInSeconds));
        }

        public virtual string GenerateRefreshToken(IEnumerable<Claim> claims)
        {
            return GenerateJwtToken(claims, _jwtSettings.RefreshTokenSecret, TimeSpan.FromSeconds(_jwtSettings.RefreshTokenExpiryInSeconds));
        }

        private string GenerateJwtToken(IEnumerable<Claim> claims, string secret, TimeSpan expiresIn)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(expiresIn),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = credentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public virtual ClaimsPrincipal? ValidateAccessToken(string token)
        {
            return ValidateJwtToken(token, _jwtSettings.AccessTokenSecret);
        }

        public virtual ClaimsPrincipal? ValidateRefreshToken(string token)
        {
            return ValidateJwtToken(token, _jwtSettings.RefreshTokenSecret);
        }

        private ClaimsPrincipal? ValidateJwtToken(string token, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secret);

            try
            {
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSettings.Audience,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, parameters, out _);
                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
