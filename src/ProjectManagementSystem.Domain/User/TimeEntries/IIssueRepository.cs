namespace ProjectManagementSystem.Domain.User.TimeEntries;

public interface IIssueRepository
{
    Task<Issue> Get(Guid id, CancellationToken cancellationToken);
    Task Save(Issue project);
}