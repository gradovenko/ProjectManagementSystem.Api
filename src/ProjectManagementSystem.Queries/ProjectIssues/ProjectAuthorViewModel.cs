namespace ProjectManagementSystem.Queries.ProjectIssues;

public sealed record ProjectAuthorViewModel
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