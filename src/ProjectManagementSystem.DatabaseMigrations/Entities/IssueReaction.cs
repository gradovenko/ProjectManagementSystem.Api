namespace ProjectManagementSystem.DatabaseMigrations.Entities;

public class IssueReaction
{
    public Guid IssueId { get; init; }
    public Guid ReactionId { get; init; }
}