namespace ProjectManagementSystem.Domain.TimeEntries;

public class TimeEntry
{
    public Guid Id { get; private set; }
    public decimal Hours { get; private set; }
    public string? Description { get; private set; }
    public DateTime? DueDate { get; private set; }
    public DateTime CreateDate { get; private set; }
    public Guid IssueId { get; private set; }
    public Guid AuthorId { get; private set; }
    public bool IsDeleted { get; private set; }

    private Guid _concurrencyToken;

    private TimeEntry() { }

    public TimeEntry(Guid id, decimal hours, string? description, DateTime? dueDate, Guid authorId, Guid userId)
    {
        Id = id;
        Hours = hours;
        Description = description;
        DueDate = dueDate;
        CreateDate = DateTime.UtcNow;
        IssueId = authorId;
        AuthorId = userId;
        _concurrencyToken = Guid.NewGuid();
    }
    
    public void Delete()
    {
        IsDeleted = true;
        _concurrencyToken = Guid.NewGuid();
    }
}