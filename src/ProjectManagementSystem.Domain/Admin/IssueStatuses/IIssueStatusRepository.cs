namespace ProjectManagementSystem.Domain.Admin.IssueStatuses;

public interface IIssueStatusRepository
{
    Task<IssueStatus> Get(Guid id, CancellationToken cancellationToken);

    Task Save(IssueStatus issuePriority);
}