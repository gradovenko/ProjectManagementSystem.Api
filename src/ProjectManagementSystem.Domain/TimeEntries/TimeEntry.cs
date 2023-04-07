namespace ProjectManagementSystem.Domain.TimeEntries;

public class TimeEntry
{
    public Guid Id { get; private set; }
    public decimal Hours { get; private set; }
    public string? Description { get; private set; }
    public DateTime? DueDate { get; private set; }
    public DateTime CreateDate { get; private set; }
    public Guid IssueId { get; private set; }
    public Guid UserId { get; private set; }
    public bool IsDeleted { get; private set; }

    // private List<INotification> _domainEvents = new();
    // public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    private Guid _concurrencyToken;

    public TimeEntry(Guid id, decimal hours, string? description, DateTime? dueDate, Guid issueId, Guid userId)
    {
        Id = id;
        Hours = hours;
        Description = description;
        DueDate = dueDate;
        CreateDate = DateTime.UtcNow;
        IssueId = issueId;
        UserId = userId;
        _concurrencyToken = Guid.NewGuid();
        
        // _domainEvents.Add(new TimeEntryCreatedDomainEvent
        // {
        //     Id = id,
        //     Content = $"Somebody added {hours}h of time spent at {dueDate} {CreateDate} days ago"
        // });
    }
    
    public void Delete()
    {
        IsDeleted = true;
        _concurrencyToken = Guid.NewGuid();
    }
}