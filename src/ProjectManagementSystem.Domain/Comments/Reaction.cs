namespace ProjectManagementSystem.Domain.Comments;

public sealed class Reaction
{
    public Guid Id { get; init; }
    public string Unicode { get; init; } = null!;
    public string Description { get; init; } = null!;
}