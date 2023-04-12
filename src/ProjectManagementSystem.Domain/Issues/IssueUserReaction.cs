namespace ProjectManagementSystem.Domain.Issues;

public sealed class IssueUserReaction
{
    public Guid IssueUserReactionId { get; init; }
    public Guid IssueId { get; init; }
    //public Issue Issue { get; init; } = null!;
    public Guid UserId { get; init; }
    //public User User { get; init; } = null!;
    public Guid ReactionId { get; init; }
    //public Reaction Reaction { get; init; } = null!;

    public IssueUserReaction(Guid issueId, Guid userId, Guid reactionId)
    {
        IssueUserReactionId = Guid.NewGuid();
        IssueId = issueId;
        UserId = userId;
        ReactionId = reactionId;
    }
}