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

    private List<IssueUserReaction> _issueUserReactions = new();
    public IReadOnlyCollection<IssueUserReaction> IssueUserReactions => _issueUserReactions.AsReadOnly();

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

    public void AddReaction(User user, Reaction reaction)
    {
        if (_issueUserReactions.Any(o => o.UserId == user.Id && o.ReactionId == reaction.Id))
            throw new InvalidOperationException("User already added this reaction to the issue");
        _issueUserReactions.Add(new IssueUserReaction(Id, user.Id, reaction.Id));
        
        UpdateDate = DateTime.UtcNow;
        _concurrencyToken = Guid.NewGuid();
    }

    public void RemoveReaction(User user, Reaction reaction)
    {
        var reactionToRemove =
            _issueUserReactions.SingleOrDefault(o => o.UserId == user.Id && o.ReactionId == reaction.Id);
        if (reactionToRemove == null)
            throw new InvalidOperationException("User has already removed this reaction from the issue, or the issue user reaction does not exist");
        _issueUserReactions.Remove(reactionToRemove);

        UpdateDate = DateTime.UtcNow;
        _concurrencyToken = Guid.NewGuid();
    }
}