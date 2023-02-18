namespace ProjectManagementSystem.Queries.Infrastructure.User.ProjectTimeEntries;

internal sealed class TimeEntry
{
    public Guid Id { get; }
    public decimal Hours { get; }
    public string Description { get; }
    public DateTime DueDate { get; }
    public DateTime CreateDate { get; }
    public DateTime? UpdateDate { get; }
    public Guid ProjectId { get; }
    public Project Project { get; }
    public Guid IssueId { get; }
    public Issue Issue { get; }
    public Guid UserId { get; }
    public User User { get; }
    public Guid ActivityId { get; }
    public TimeEntryActivity Activity { get; }
}