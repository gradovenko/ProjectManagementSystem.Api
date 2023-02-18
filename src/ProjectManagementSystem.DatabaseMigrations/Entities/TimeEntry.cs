namespace ProjectManagementSystem.DatabaseMigrations.Entities;

public sealed class TimeEntry
{
    public Guid TimeEntryId { get; set; }
    public decimal Hours { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }
    public Guid IssueId { get; set; }
    public Issue Issue { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid ActivityId { get; set; }
    public TimeEntryActivity Activity { get; set; }
    public Guid ConcurrencyStamp { get; set; }
}