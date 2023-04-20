namespace ProjectManagementSystem.Queries.User.IssueComments;

public sealed record AuthorViewModel
{
    /// <summary>
    /// User identifier
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// User name
    /// </summary>
    public string Name { get; init; } = null!;
}