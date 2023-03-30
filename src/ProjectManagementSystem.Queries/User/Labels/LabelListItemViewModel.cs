namespace ProjectManagementSystem.Queries.User.Labels;

public sealed record LabelListItemViewModel
{
    public Guid Id { get; init; }
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public string BackgroundColor { get; init; } = null!;
}