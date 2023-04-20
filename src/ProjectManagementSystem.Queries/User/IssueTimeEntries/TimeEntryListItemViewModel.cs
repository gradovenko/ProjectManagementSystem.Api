namespace ProjectManagementSystem.Queries.User.IssueTimeEntries;

public sealed record TimeEntryListItemViewModel
{
    /// <summary>
    /// Time entry identifier
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Time entry hours
    /// </summary>
    public decimal Hours { get; init; }
    
    /// <summary>
    /// Time entry description
    /// </summary>
    public string? Description { get; init; }
    
    /// <summary>
    /// Time entry due date
    /// </summary>
    public DateTime? DueDate { get; init; }
    
    /// <summary>
    /// Time entry create date
    /// </summary>
    public DateTime CreateDate { get; init; }
    
    /// <summary>
    /// Time entry is deleted flag
    /// </summary>
    public bool IsDeleted { get; init; }
    
    /// <summary>
    /// Time entry author
    /// </summary>
    public AuthorViewModel Author { get; init; } = null!;
}