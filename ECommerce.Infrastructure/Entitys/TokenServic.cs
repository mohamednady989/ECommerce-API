using ECommerce.Application.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.Infrastructure.Entitys
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }
        public string CreateToken(string userId, string email, string userName, IReadOnlyList<string> roles)
        {
            var claims = new List<Claim>()
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, userId),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, email),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, userName)
            };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
            // ... rest of the token creation logic
            var secKey = _jwtSettings.SecretKey;
            if (string.IsNullOrEmpty(secKey))
            {
                throw new InvalidOperationException("JWT Secret Key is not configured");
            }
            if (secKey.Length < 32)
            {
                throw new InvalidOperationException("JWT Secret Key must be at least 32 characters long");
            }
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secKey));
            var credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwtSettings.ExpirationInDays),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    public class JwtSettings
    {
        public string SecretKey { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public int ExpirationInDays { get; set; }
    }


}
