using MediatR;

namespace ProjectManagementSystem.Domain.Issues.Commands;

public sealed record ReopenIssueCommand : IRequest<ReopenIssueCommandResultState>
{
    public Guid IssueId { get; init; }
    public Guid UserId { get; init; }
}

public enum ReopenIssueCommandResultState
{
    UserNotFound,
    IssueNotFound,
    IssueReopened
}