namespace ProjectManagementSystem.Queries.User.IssueReactions;

public sealed record ReactionListItemViewModel
{
    public Guid Id { get; init; }
    public string Emoji { get; init; } = null!;
    public string Name { get; init; } = null!;
    public string Category { get; init; } = null!;
}