namespace ProjectManagementSystem.Domain.User.Issues;

public interface IIssuePriorityRepository
{
    Task<IssuePriority> Get(Guid id, CancellationToken cancellationToken);
}