using MediatR;

namespace ProjectManagementSystem.Domain.Issues.Commands;

public class AddLabelToIssueCommand : IRequest<AddLabelToIssueCommandResultState>
{
    public Guid LabelId { get; init; }
    public Guid IssueId { get; init; }
    public Guid UserId { get; init; }
}

public enum AddLabelToIssueCommandResultState
{
    IssueNotFound,
    UserNotFound,
    LabelNotFound,
    IssueUserLabelAlreadyExists,
    LabelToIssueAdded
}