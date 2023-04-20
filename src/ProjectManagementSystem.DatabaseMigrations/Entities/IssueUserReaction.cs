namespace ProjectManagementSystem.DatabaseMigrations.Entities;

internal sealed record IssueUserReaction
{
    public Guid IssueId { get; init; }
    public Issue Issue { get; init; } = null!;
    public Guid UserId { get; init; }
    public User User { get; init; } = null!;
    public Guid ReactionId { get; init; }
    public Reaction Reaction { get; init; } = null!;
}