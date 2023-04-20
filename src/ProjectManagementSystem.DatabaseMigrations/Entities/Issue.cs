namespace ProjectManagementSystem.DatabaseMigrations.Entities;

internal sealed record Issue
{
    public Guid IssueId { get; init; }
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public string State { get; init; } = null!;
    public DateTime CreateDate { get; init; }
    public DateTime UpdateDate { get; init; }
    public DateTime? DueDate { get; init; }
    public Guid? UserIdWhoClosed { get; init; }
    public User? UserWhoClosed { get; init; }
    public Guid ProjectId { get; init; }
    public Project Project { get; init; } = null!;
    public User Author { get; init; } = null!;
    public Guid AuthorId { get; init; }
    public IEnumerable<TimeEntry> TimeEntries { get; init; } = null!;
    public IEnumerable<Comment> Comments { get; init; } = null!;
    public IEnumerable<IssueAssignee> IssueAssignees { get; init; } = null!;
    public IEnumerable<IssueLabel> IssueLabels { get; init; } = null!;
    public IEnumerable<IssueUserReaction> IssueUserReactions { get; init; } = null!;
    public Guid ConcurrencyToken { get; init; }
}