using MediatR;

namespace ProjectManagementSystem.Domain.Issues.Commands;

public class RemoveLabelFromIssueCommand : IRequest<RemoveLabelFromIssueCommandResultState>
{
    public Guid LabelId { get; init; }
    public Guid IssueId { get; init; }
    public Guid UserId { get; init; }
}

public enum RemoveLabelFromIssueCommandResultState
{
    IssueNotFound,
    UserNotFound,
    LabelNotFound,
    IssueUserLabelNotFound,
    LabelFromIssueRemoved
}