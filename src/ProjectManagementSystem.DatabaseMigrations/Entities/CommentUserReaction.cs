namespace ProjectManagementSystem.DatabaseMigrations.Entities;

internal sealed record CommentUserReaction
{
    public Guid CommentId { get; init; }
    public Comment Comment { get; init; } = null!;
    public Guid UserId { get; init; }
    public User User { get; init; } = null!;
    public Guid ReactionId { get; init; }
    public Reaction Reaction { get; init; } = null!;
}