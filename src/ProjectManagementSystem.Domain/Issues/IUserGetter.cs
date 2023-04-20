namespace ProjectManagementSystem.Domain.Issues;

public interface IUserGetter
{
    Task<User?> Get(Guid id, CancellationToken cancellationToken);
}