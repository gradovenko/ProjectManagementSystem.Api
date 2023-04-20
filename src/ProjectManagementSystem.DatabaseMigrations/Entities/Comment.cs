namespace ProjectManagementSystem.DatabaseMigrations.Entities;

internal sealed record Comment
{
    public Guid CommentId { get; init; }
    public string Content { get; init; } = null!;
    public bool IsDeleted { get; init; }
    public DateTime CreateDate { get; init; }
    public DateTime UpdateDate { get; init; }
    public User Author { get; init; } = null!;
    public Guid AuthorId { get; init; }
    public Issue Issue { get; init; } = null!;
    public Guid IssueId { get; init; }
    public Comment? ParentComment { get; init; }
    public Guid? ParentCommentId { get; init; }
    public IEnumerable<Comment> ChildComments { get; init; } = null!;
    public IEnumerable<CommentUserReaction> CommentUserReactions { get; init; } = null!;
    public Guid ConcurrencyToken { get; init; }
}