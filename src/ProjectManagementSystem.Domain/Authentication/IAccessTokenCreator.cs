namespace ProjectManagementSystem.Domain.Authentication;

public interface IAccessTokenCreator
{
    Task<AccessToken> Create(User user, CancellationToken cancellationToken);
}