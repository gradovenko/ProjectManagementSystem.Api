using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectManagementSystem.Domain.Authentication;

namespace ProjectManagementSystem.Infrastructure.Authentication
{
    public sealed class JwtAccessTokenFactory : IJwtAccessTokenFactory
    {
        private readonly JwtOptions _jwtOptions;

        public JwtAccessTokenFactory(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        
        public async Task<AccessToken> Create(Domain.Authentication.User user, CancellationToken cancellationToken)
        {
            var jwtSecurityToken = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType).Claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresInMinutes),
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)), SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return new AccessToken(token, TimeSpan.FromMinutes(_jwtOptions.ExpiresInMinutes));
        }
    }
}