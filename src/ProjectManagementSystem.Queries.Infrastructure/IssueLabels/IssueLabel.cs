namespace ProjectManagementSystem.Queries.Infrastructure.IssueLabels;

internal sealed record IssueLabel
{
    public Guid IssueId { get; init; }
    public Guid LabelId { get; init; }
}