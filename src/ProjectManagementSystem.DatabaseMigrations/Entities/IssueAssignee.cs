namespace ProjectManagementSystem.DatabaseMigrations.Entities;

internal sealed record IssueAssignee
{
    public Guid IssueId { get; init; }
    public Issue Issue { get; init; } = null!;
    public Guid AssigneeId { get; init; }
    public User Assignee { get; init; } = null!;
}