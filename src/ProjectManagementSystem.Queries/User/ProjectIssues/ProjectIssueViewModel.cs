namespace ProjectManagementSystem.Queries.User.ProjectIssues;

public sealed record ProjectIssueViewModel
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
    /// Issue author
    /// </summary>
    public ProjectAuthorViewModel Author { get;  init; } = null!;

    /// <summary>
    /// Issue assignees
    /// </summary>
    public IEnumerable<ProjectAssigneeViewModel> Assignees { get; init; }
}