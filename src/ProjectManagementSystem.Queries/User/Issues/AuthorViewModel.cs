namespace ProjectManagementSystem.Queries.User.Issues;

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