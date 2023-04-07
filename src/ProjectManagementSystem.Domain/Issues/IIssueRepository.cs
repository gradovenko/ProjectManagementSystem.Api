namespace ProjectManagementSystem.Domain.Issues;

public interface IIssueRepository
{
    Task<Issue?> Get(Guid id, CancellationToken cancellationToken);
    Task Save(Issue issue, CancellationToken cancellationToken);
}