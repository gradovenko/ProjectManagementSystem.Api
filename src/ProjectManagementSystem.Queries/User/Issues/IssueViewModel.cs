namespace ProjectManagementSystem.Queries.User.Issues;

public sealed record IssueViewModel
{
    /// <summary>
    /// Issue identifier
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Issue title
    /// </summary>
    public string Title { get; init; } = null!;
        
    /// <summary>
    /// Issue description
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Issue state
    /// </summary>
    public string State { get; init; } = null!;

    /// <summary>
    /// Issue create date
    /// </summary>
    public DateTime CreateDate { get; init; }
        
    /// <summary>
    /// Issue update date
    /// </summary>
    public DateTime UpdateDate { get; init; }

    /// <summary>
    /// Issue due date
    /// </summary>
    public DateTime? DueDate { get; init; }
    
    /// <summary>
    /// Issue project
    /// </summary>
    public ProjectViewModel Project { get;  init; } = null!;

    /// <summary>
    /// Issue author
    /// </summary>
    public AuthorViewModel Author { get;  init; } = null!;

    /// <summary>
    /// Issue assignees
    /// </summary>
    public IEnumerable<AssigneeViewModel> Assignees { get; init; } = null!;
}