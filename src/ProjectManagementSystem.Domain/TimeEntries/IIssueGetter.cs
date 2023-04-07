namespace ProjectManagementSystem.Domain.TimeEntries;

public interface IIssueGetter
{
    Task<Issue?> Get(Guid id, CancellationToken cancellationToken);
}