using MediatR;

namespace ProjectManagementSystem.Domain.Issues.Commands;

public sealed record CloseIssueCommand : IRequest<CloseIssueCommandResultState>
{
    public Guid IssueId { get; init; }
    public Guid UserId { get; init; }
}

public enum CloseIssueCommandResultState
{
    UserNotFound,
    IssueNotFound,
    IssueClosed
}