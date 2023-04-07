namespace ProjectManagementSystem.DatabaseMigrations.Entities;

public class IssueUser
{
    public Guid IssueId { get; init; }
    public Guid UserId { get; init; }
}