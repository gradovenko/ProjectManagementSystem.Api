namespace ProjectManagementSystem.Domain.User.Issues;

public interface IUserRepository
{
    Task<User> Get(Guid id, CancellationToken cancellationToken);
}