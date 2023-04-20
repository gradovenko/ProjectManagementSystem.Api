namespace ProjectManagementSystem.Queries.User.Issues;

public sealed record ProjectViewModel
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