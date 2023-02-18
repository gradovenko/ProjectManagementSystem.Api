namespace ProjectManagementSystem.Domain.Authentication;

public interface IJwtAccessTokenFactory
{
    Task<AccessToken> Create(User user, CancellationToken cancellationToken);
}