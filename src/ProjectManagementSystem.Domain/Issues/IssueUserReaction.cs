namespace ProjectManagementSystem.Domain.Issues;

public sealed class IssueUserReaction
{
    public Guid IssueId { get; init; }
    public Guid UserId { get; init; }
    public Guid ReactionId { get; init; }

    private IssueUserReaction() { }

    public IssueUserReaction(Guid issueId, Guid userId, Guid reactionId)
    {
        IssueId = issueId;
        UserId = userId;
        ReactionId = reactionId;
    }
}