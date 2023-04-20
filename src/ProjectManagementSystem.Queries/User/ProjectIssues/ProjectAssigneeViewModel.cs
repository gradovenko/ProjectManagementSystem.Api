namespace ProjectManagementSystem.Queries.User.ProjectIssues;

public sealed record ProjectAssigneeViewModel
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