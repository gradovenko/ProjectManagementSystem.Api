namespace ProjectManagementSystem.DatabaseMigrations.Entities;

internal sealed record TimeEntry
{
    public Guid TimeEntryId { get; init; }
    public decimal Hours { get; init; }
    public string? Description { get; init; }
    public DateTime? DueDate { get; init; }
    public DateTime CreateDate { get; init; }
    public Guid IssueId { get; init; }
    public Issue Issue { get; init; } = null!;
    public Guid AuthorId { get; init; }
    public User Author { get; init; } = null!;
    public bool IsDeleted { get; init; }
    public Guid ConcurrencyToken { get; init; }
}