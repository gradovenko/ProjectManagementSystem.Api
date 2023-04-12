namespace ProjectManagementSystem.DatabaseMigrations.Entities;

internal sealed record Reaction
{
    public Guid ReactionId { get; init; }
    public string Unicode { get; init; } = null!;
    public string Description { get; init; } = null!;
    public IEnumerable<IssueUserReaction> IssueUserReactions { get; init; } = null!;
    public IEnumerable<CommentUserReaction> CommentReactions { get; init; } = null!;
}