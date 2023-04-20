namespace ProjectManagementSystem.Queries.Infrastructure.Issues;

internal sealed record IssueAssignee
{
    public Guid IssueId { get; init; } 
    public Guid AssigneeId { get; init; }
}