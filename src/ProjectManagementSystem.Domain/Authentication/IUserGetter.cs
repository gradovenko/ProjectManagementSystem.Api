namespace ProjectManagementSystem.Domain.Authentication;

public interface IUserGetter
{
    Task<User> Get(Guid id, CancellationToken cancellationToken);
    Task<User?> GetByLogin(string login, CancellationToken cancellationToken);
}