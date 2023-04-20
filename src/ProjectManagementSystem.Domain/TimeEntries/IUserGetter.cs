namespace ProjectManagementSystem.Domain.TimeEntries;

public interface IUserGetter
{
    Task<User?> Get(Guid id, CancellationToken cancellationToken);
}