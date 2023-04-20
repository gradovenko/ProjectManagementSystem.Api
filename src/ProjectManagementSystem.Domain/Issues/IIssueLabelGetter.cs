namespace ProjectManagementSystem.Domain.Issues;

public interface IIssueLabelGetter
{
    Task<IssueLabel?> Get(Guid issueId, Guid labelId, CancellationToken cancellationToken);
}