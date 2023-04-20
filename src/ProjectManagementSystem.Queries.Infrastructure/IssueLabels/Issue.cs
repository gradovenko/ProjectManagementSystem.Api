namespace ProjectManagementSystem.Queries.Infrastructure.IssueLabels;

internal sealed record Issue
{
    public Guid IssueId { get; init; }
    public IEnumerable<Label> Labels { get; init; } = null!;
}