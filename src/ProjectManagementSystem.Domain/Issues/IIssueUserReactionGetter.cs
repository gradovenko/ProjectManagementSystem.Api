namespace ProjectManagementSystem.Domain.Issues;

public interface IIssueUserReactionGetter
{
    Task<IssueUserReaction?> Get(Guid issueId, Guid userId, Guid reactionId, CancellationToken cancellationToken);
}