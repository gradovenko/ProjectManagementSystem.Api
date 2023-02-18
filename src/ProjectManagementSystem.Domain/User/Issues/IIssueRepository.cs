namespace ProjectManagementSystem.Domain.User.Issues;

public interface IIssueRepository
{
    Task<Issue> Get(Guid id, CancellationToken cancellationToken);
}