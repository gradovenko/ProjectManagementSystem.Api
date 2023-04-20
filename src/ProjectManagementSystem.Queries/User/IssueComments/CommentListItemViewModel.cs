namespace ProjectManagementSystem.Queries.User.IssueComments;

public sealed record CommentListItemViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Content { get; init; } = null!;
    
    /// <summary>
    /// 
    /// </summary>
    public DateTime CreateDate { get; init; }
    
    
    /// <summary>
    /// 
    /// </summary>
    public DateTime UpdateDate { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public AuthorViewModel Author { get; init; } = null!;
    
    /// <summary>
    /// 
    /// </summary>
    public Guid? ParentCommentId { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<CommentListItemViewModel>? ChildComments { get; init; } = null!;
}