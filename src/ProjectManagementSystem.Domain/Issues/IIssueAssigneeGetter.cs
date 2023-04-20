namespace ProjectManagementSystem.Domain.Issues;

public interface IIssueAssigneeGetter
{
    Task<IssueAssignee?> Get(Guid issueId, Guid assigneeId, CancellationToken cancellationToken);
}