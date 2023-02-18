namespace ProjectManagementSystem.Domain.User.Issues;

public interface IIssueStatusRepository
{
    Task<IssueStatus> Get(Guid id, CancellationToken cancellationToken);
}