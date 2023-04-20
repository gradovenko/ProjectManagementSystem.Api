namespace ProjectManagementSystem.Queries.Infrastructure.IssueReactions;

internal sealed record IssueUserReaction
{
    public Guid IssueId { get; init; }
    public Guid ReactionId { get; init; }
}