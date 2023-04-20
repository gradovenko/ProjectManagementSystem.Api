namespace ProjectManagementSystem.Domain.Issues;

public sealed class IssueLabel
{
    public Guid IssueId { get; init; }
    //public Issue Issue { get; init; } = null!;
    public Guid LabelId { get; init; }
    //public Label Label { get; init; } = null!;
    
    private IssueLabel() { }

    public IssueLabel(Guid issueId, Guid labelId)
    {
        IssueId = issueId;
        LabelId = labelId;
    }
}