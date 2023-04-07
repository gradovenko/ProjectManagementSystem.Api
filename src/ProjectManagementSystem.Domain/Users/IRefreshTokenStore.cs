namespace ProjectManagementSystem.Domain.Users;

public interface IRefreshTokenStore
{
    Task ExpireAllTokens(Guid userId, CancellationToken cancellationToken);
}