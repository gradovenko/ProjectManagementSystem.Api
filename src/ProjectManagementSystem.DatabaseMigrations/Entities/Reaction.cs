namespace ProjectManagementSystem.DatabaseMigrations.Entities;

internal sealed record Reaction
{
    public string ReactionId { get; init; } = null!;
    public IEnumerable<Issue> Issues { get; init; } = null!;
}