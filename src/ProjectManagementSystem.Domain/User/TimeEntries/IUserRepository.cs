namespace ProjectManagementSystem.Domain.User.TimeEntries;

public interface IUserRepository
{ 
    Task<User> Get(Guid id, CancellationToken cancellationToken);
}