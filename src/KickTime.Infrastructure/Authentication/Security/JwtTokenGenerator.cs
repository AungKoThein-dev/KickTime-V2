using KickTime.Application.Authentication.Interfaces;
using KickTime.Core.Constants;
using KickTime.Core.Entities;
using KickTime.Infrastructure.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using System.Text;
using System.Reflection.Metadata.Ecma335;

namespace KickTime.Infrastructure.Authentication.Security
{
    public sealed class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;
        private static readonly JwtSecurityTokenHandler TokenHandler = new();
        public JwtTokenGenerator(
            IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value;
        }

        public string GenerateToken(User user, string roleName)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            ValidateJwtOptions();

            var secret = _jwtOptions.Key;
            var issuer = _jwtOptions.Issuer;
            var audience = _jwtOptions.Audience;
            var expiresMinutes = _jwtOptions.ExpireMinutes;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = CreateClaims(user, roleName);

            var token = CreateToken(issuer, audience, claims, DateTime.UtcNow, expiresMinutes, signingCredentials);

            return TokenHandler.WriteToken(token);

        }

        private void ValidateJwtOptions()
        {
            if (string.IsNullOrWhiteSpace(_jwtOptions.Key))
                throw new InvalidOperationException(Messages.JWTKeyNotConfigured);
            if (string.IsNullOrWhiteSpace(_jwtOptions.Issuer))
                throw new InvalidOperationException(Messages.JWTIssuerNotConfigured);
            if (string.IsNullOrWhiteSpace(_jwtOptions.Audience))
                throw new InvalidOperationException(Messages.JWTAudienceNotConfigured);
            if (_jwtOptions.ExpireMinutes <= 0)
                throw new InvalidOperationException(Messages.JWTExpireMinutesNotConfigured);
        }

        private IReadOnlyCollection<Claim> CreateClaims(User user, string roleName)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name ?? user.Email),
                new Claim(ClaimTypes.Role, roleName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

        }

        private JwtSecurityToken CreateToken(string issuer, string audience, IEnumerable<Claim> claims, DateTime now, int expiresMinutes, SigningCredentials signingCredentials)
        {
            return new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(expiresMinutes),
                signingCredentials: signingCredentials
            );
        }
    }
}
