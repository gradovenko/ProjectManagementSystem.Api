namespace ProjectManagementSystem.DatabaseMigrations.Entities;

internal sealed record Reaction
{
    public Guid ReactionId { get; init; }
    public string Emoji { get; init; } = null!;
    public string Name { get; init; } = null!;
    public string Category { get; init; } = null!;
    public bool IsDeleted { get; init; }
    public Guid ConcurrencyToken { get; init; }
    public IEnumerable<IssueUserReaction> IssueUserReactions { get; init; } = null!;
    public IEnumerable<CommentUserReaction> CommentReactions { get; init; } = null!;
}