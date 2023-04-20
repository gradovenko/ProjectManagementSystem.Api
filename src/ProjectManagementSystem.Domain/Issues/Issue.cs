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
    public Guid? UserIdWhoClosed { get; private set; }
    public Guid ProjectId { get; private set; }
    public Guid AuthorId { get; private set; }

    private List<IssueAssignee> _issueAssignees = new();
    public IReadOnlyCollection<IssueAssignee> IssueAssignees => _issueAssignees.AsReadOnly();

    private List<IssueLabel> _issueLabels = new();
    public IReadOnlyCollection<IssueLabel> IssueLabels => _issueLabels.AsReadOnly();

    private List<IssueUserReaction> _issueUserReactions = new();
    public IReadOnlyCollection<IssueUserReaction> IssueUserReactions => _issueUserReactions.AsReadOnly();

    private Guid _concurrencyToken;

    private Issue() { }

    public Issue(Guid id, string title, string? description, DateTime? dueDate, 
        Guid projectId, Guid authorId, IEnumerable<IssueAssignee> assignees, IEnumerable<IssueLabel> labels)
    {
        Id = id;
        Title = title;
        Description = description;
        State = IssueState.Open;
        CreateDate = UpdateDate = DateTime.UtcNow;
        DueDate = dueDate;
        ProjectId = projectId;
        AuthorId = authorId;
        _issueAssignees.AddRange(assignees);
        _issueLabels.AddRange(labels);
        _concurrencyToken = Guid.NewGuid();
    }

    public void Update(string title, string? description)
    {
        Title = title;
        Description = description;
        _concurrencyToken = Guid.NewGuid();
    }

    public void Close(Guid userIdWhoClosed)
    {
        State = IssueState.Closed;
        UpdateDate = DateTime.UtcNow;
        UserIdWhoClosed = userIdWhoClosed;
        _concurrencyToken = Guid.NewGuid();
    }

    public void Reopen()
    {
        State = IssueState.Open;
        UpdateDate = DateTime.UtcNow;
        UserIdWhoClosed = null;
        _concurrencyToken = Guid.NewGuid();
    }

    public void AddLabel(Guid userId, Guid labelId)
    {
        if (_issueLabels.Any(o => o.LabelId == labelId))
            throw new InvalidOperationException();

        _issueLabels.Add(new IssueLabel(Id, labelId));

        UpdateDate = DateTime.UtcNow;
        _concurrencyToken = Guid.NewGuid();
    }
    
    public void RemoveLabel(Guid userId, Guid labelId)
    {
        IssueLabel? issueLabel =
            _issueLabels.Single(o => o.LabelId == labelId);

        if (issueLabel == null)
            throw new InvalidOperationException();

        _issueLabels.Remove(issueLabel);

        UpdateDate = DateTime.UtcNow;
        _concurrencyToken = Guid.NewGuid();
    }

    public void AddUserReaction(Guid userId, Guid reactionId)
    {
        if (_issueUserReactions.Any(o => o.UserId == userId && o.ReactionId == reactionId))
            throw new InvalidOperationException($"User {userId} already added this reaction {reactionId} to the issue {Id}");

        _issueUserReactions.Add(new IssueUserReaction(Id, userId, reactionId));
        
        UpdateDate = DateTime.UtcNow;
        _concurrencyToken = Guid.NewGuid();
    }

    public void RemoveUserReaction(Guid userId, Guid reactionId)
    {
        IssueUserReaction? issueUserReaction =
            _issueUserReactions.Single(o => o.UserId == userId && o.ReactionId == reactionId);

        if (issueUserReaction == null)
            throw new InvalidOperationException($"User {userId} has already removed this reaction {reactionId} from the issue, or the issue user reaction {reactionId} does not exist");

        _issueUserReactions.Remove(issueUserReaction);

        UpdateDate = DateTime.UtcNow;
        _concurrencyToken = Guid.NewGuid();
    }
}