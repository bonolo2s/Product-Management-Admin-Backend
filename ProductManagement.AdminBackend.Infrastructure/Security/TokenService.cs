using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductManagement.AdminBackend.Core.DTOs;
using ProductManagement.AdminBackend.Core.Entities;
using ProductManagement.AdminBackend.Core.Interfaces;

namespace ProductManagement.AdminBackend.Infrastructure.Security
{
    public class TokenService : ITokenService
    {
        private readonly string _jwtKey;
        private readonly string? _issuer;
        private readonly string? _audience;
        private readonly int _accessTokenMinutes;
        private readonly int _refreshTokenDays;

        public TokenService(IConfiguration config)
        {
            _jwtKey = config["Jwt:Key"] ?? throw new ArgumentException("Jwt:Key not set");
            _issuer = config["Jwt:Issuer"];
            _audience = config["Jwt:Audience"];
            _accessTokenMinutes = int.TryParse(config["Jwt:AccessTokenMinutes"], out var m) ? m : 120;
            _refreshTokenDays = int.TryParse(config["Jwt:RefreshTokenDays"], out var d) ? d : 30;
        }

        public AuthResponseDto GenerateTokens(Admin admin)
        {
            // 1) Claims
            var claims = new List<Claim>
            {
                new Claim("id", admin.Id.ToString()),
                new Claim(ClaimTypes.Role, ((int)admin.adminRole).ToString()),
                new Claim("email", admin.Email ?? string.Empty)
            };

            // 2) Signing credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 3) Create token
            var expiresAt = DateTime.UtcNow.AddMinutes(_accessTokenMinutes);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresAt,
                SigningCredentials = creds,
                Issuer = _issuer,
                Audience = _audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            // 4) Create secure refresh token (opaque string)
            var refreshToken = CreateSecureRefreshToken();

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = expiresAt
            };
        }

        private static string CreateSecureRefreshToken(int size = 64)
        {
            var randomBytes = new byte[size];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}
