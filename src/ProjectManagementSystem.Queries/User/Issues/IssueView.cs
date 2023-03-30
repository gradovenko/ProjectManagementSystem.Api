namespace ProjectManagementSystem.Queries.User.Issues;

public sealed record IssueViewModel
{
    /// <summary>
    /// Issue identifier
    /// </summary>
    public Guid Id { get; set; }
        
    /// <summary>
    /// Issue sequence number
    /// </summary>
    public long Number { get; set; }
        
    /// <summary>
    /// Issue title
    /// </summary>
    public string Title { get; set; }
        
    /// <summary>
    /// Issue description
    /// </summary>
    public string Description { get; set; }
        
    /// <summary>
    /// Issue create date
    /// </summary>
    public DateTime CreateDate { get; set; }
        
    /// <summary>
    /// Issue update date
    /// </summary>
    public DateTime? UpdateDate { get; set; }
        
    /// <summary>
    /// Issue start date
    /// </summary>
    public DateTime? StartDate { get; set; }
        
    /// <summary>
    /// Issue due date
    /// </summary>
    public DateTime? DueDate { get; set; }
        
    /// <summary>
    /// Issue tracker name
    /// </summary>
    public string TrackerName { get; set; }
        
    /// <summary>
    /// Issue status name
    /// </summary>
    public string StatusName { get; set; }
        
    /// <summary>
    /// Issue priority name
    /// </summary>
    public string PriorityName { get; set; }
        
    /// <summary>
    /// Issue author name
    /// </summary>
    public string AuthorName { get;  set; }
        
    /// <summary>
    /// Issue assignee name
    /// </summary>
    public string AssigneeName { get; set; }
}