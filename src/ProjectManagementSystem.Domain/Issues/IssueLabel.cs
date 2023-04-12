namespace ProjectManagementSystem.Domain.Issues;

internal sealed record IssueLabel
{
    public Guid IssueId { get; init; }
    public Issue Issue { get; init; } = null!;
    public Guid LabelId { get; init; }
    public Label Label { get; init; } = null!;
}