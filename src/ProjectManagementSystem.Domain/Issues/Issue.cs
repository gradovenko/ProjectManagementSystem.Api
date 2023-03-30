namespace ProjectManagementSystem.Domain.Issues;

public sealed class Issue
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public IssueState State { get; private set; }
    public DateTime CreateDate { get; private set; }
    public DateTime UpdateDate { get; private set; }
    public DateTime? DueDate { get; private set; }
    //public DateTime? CloseDate { get; private set; }
    public Guid? ClosedByUserId { get; private set; }
    public Guid ProjectId { get; private set; }
    public Guid AuthorId { get; private set; }

    private List<User> _assignees = new();
    public IReadOnlyCollection<User> Assignees => _assignees.AsReadOnly();

    private List<Label> _labels = new();
    public IReadOnlyCollection<Label> Labels => _labels.AsReadOnly();

    private List<Reaction> _reactions = new();
    public IReadOnlyCollection<Reaction> Reactions => _reactions.AsReadOnly();

    private Guid _concurrencyToken;

    private Issue() { }

    public Issue(Guid id, string title, string? description, DateTime? dueDate, 
        Guid projectId, Guid authorId, IEnumerable<User> assignees, IEnumerable<Label> labels)
    {
        Id = id;
        Title = title;
        Description = description;
        State = IssueState.Open;
        CreateDate = UpdateDate = DateTime.UtcNow;
        DueDate = dueDate;
        ProjectId = projectId;
        AuthorId = authorId;
        _assignees.AddRange(assignees);
        _labels.AddRange(labels);
        _concurrencyToken = Guid.NewGuid();
    }

    public void Update(string title, string? description)
    {
        Title = title;
        Description = description;
        _concurrencyToken = Guid.NewGuid();
    }

    public void Close(Guid closedByUserId)
    {
        State = IssueState.Closed;
        UpdateDate = DateTime.UtcNow;
        ClosedByUserId = closedByUserId;
        _concurrencyToken = Guid.NewGuid();
    }

    public void Reopen()
    {
        State = IssueState.Open;
        UpdateDate = DateTime.UtcNow;
        ClosedByUserId = null;
        _concurrencyToken = Guid.NewGuid();
    }

    public void AddReaction(Reaction reaction)
    {
        _reactions.Add(reaction);
        UpdateDate = DateTime.UtcNow;
        _concurrencyToken = Guid.NewGuid();
    }

    public void RemoveReaction(Reaction reaction)
    {
        _reactions.Remove(reaction);
        UpdateDate = DateTime.UtcNow;
        _concurrencyToken = Guid.NewGuid();
    }
    
    // public void AddTimeEntry(TimeEntry timeEntry)
    // {
    //     _timeEntries.Add(timeEntry);
    //     UpdateDate = DateTime.UtcNow;
    //     _concurrencyToken = Guid.NewGuid();
    // }
    //
    // public void RemoveTimeEntry(TimeEntry timeEntry)
    // {
    //     _timeEntries.Add(timeEntry);
    //     UpdateDate = DateTime.UtcNow;
    //     _concurrencyToken = Guid.NewGuid();
    // }
}