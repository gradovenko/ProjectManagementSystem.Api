namespace ProjectManagementSystem.Domain.Admin.IssuePriorities;

public interface IIssuePriorityRepository
{
    Task<IssuePriority> Get(Guid id, CancellationToken cancellationToken);

    Task Save(IssuePriority issuePriority);
}