namespace ProjectManagementSystem.Domain.Issues;

public sealed class IssueAssignee
{
    public Guid IssueId { get; init; }
   // public Issue Issue { get; init; } = null!;
    public Guid AssigneeId { get; init; }
    //public User Assignee { get; init; } = null!;
    
    private IssueAssignee() { }

    public IssueAssignee(Guid issueId, Guid assigneeId)
    {
        IssueId = issueId;
        AssigneeId = assigneeId;
    }
}