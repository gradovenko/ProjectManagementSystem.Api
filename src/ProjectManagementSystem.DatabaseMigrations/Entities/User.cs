namespace ProjectManagementSystem.DatabaseMigrations.Entities;

internal sealed record User
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string PasswordHash { get; init; } = null!;
    public string Role { get; init; } = null!;
    public string State { get; init; } = null!;
    public DateTime CreateDate { get; init; }
    public DateTime UpdateDate { get; init; }
    public IEnumerable<Issue> CreatedIssues { get; init; } = null!;
    public IEnumerable<Issue> ClosedIssues { get; init; } = null!;
    public IEnumerable<TimeEntry> TimeEntries { get; init; } = null!;
    public IEnumerable<Comment> Comments { get; init; } = null!;
    public IEnumerable<IssueAssignee> IssueAssignees { get; init; } = null!;
    public IEnumerable<IssueUserReaction> IssueUserReactions { get; init; } = null!;
    public IEnumerable<CommentUserReaction> CommentUserReactions { get; init; } = null!;
    public Guid ConcurrencyToken { get; init; }
}