namespace ProjectManagementSystem.Domain.Authentication;

public interface IRefreshTokenStore
{
    Task<string> Add(Guid userId, CancellationToken cancellationToken);
    Task<(string Value, Guid UserId)?> Reissue(string refreshToken, CancellationToken cancellationToken);
}