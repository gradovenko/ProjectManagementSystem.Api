namespace ProjectManagementSystem.Queries.Infrastructure.IssueReactions;

internal sealed record Issue
{
    public Guid IssueId { get; init; }
    public IEnumerable<Reaction> Reactions { get; init; } = null!;
}