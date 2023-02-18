namespace ProjectManagementSystem.Domain.User.Accounts;

public interface IUserRepository
{
    Task<User> Get(Guid id, CancellationToken cancellationToken);
    Task<User> GetByName(string name, CancellationToken cancellationToken);
    Task<User> GetByEmail(string email, CancellationToken cancellationToken);
    Task Save(User user);
}