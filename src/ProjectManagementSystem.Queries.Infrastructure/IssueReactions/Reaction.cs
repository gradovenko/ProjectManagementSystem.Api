namespace ProjectManagementSystem.Queries.Infrastructure.IssueReactions;

internal sealed record Reaction
{
    public Guid ReactionId { get; init; }
    public string Emoji { get; init; } = null!;
    public string Name { get; init; } = null!;
    public string Category { get; private set; } = null!;
    public IEnumerable<Issue> Issues { get; init; } = null!;
}