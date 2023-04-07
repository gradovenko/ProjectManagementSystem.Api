using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectManagementSystem.Domain.Authentication;

namespace ProjectManagementSystem.Infrastructure.Authentication;

public class JwtAccessTokenCreator : IAccessTokenCreator
{
    private readonly JwtAuthOptions _authOptions;

    public JwtAccessTokenCreator(IOptions<JwtAuthOptions> authOptions)
    {
        _authOptions = authOptions.Value ?? throw new ArgumentNullException(nameof(authOptions));
    }

    public Task<AccessToken> Create(User user, CancellationToken cancellationToken)
    {
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _authOptions.Issuer,
            audience: _authOptions.Audience,
            claims: new ClaimsIdentity(new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Email, user.Email)
            }, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType).Claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_authOptions.LifeTimeInMinutes),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.SecretKey)), SecurityAlgorithms.HmacSha256));

        string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return Task.FromResult(new AccessToken(token, TimeSpan.FromMinutes(_authOptions.LifeTimeInMinutes)));
    }
}