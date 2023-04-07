namespace ProjectManagementSystem.Queries.Issues;

public sealed record AssigneeViewModel
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