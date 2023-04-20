using MediatR;

namespace ProjectManagementSystem.Domain.Issues.Commands;

public sealed record RemoveReactionFromIssueCommand : IRequest<RemoveReactionFromIssueCommandResultState>
{
    public Guid ReactionId { get; init; }
    public Guid IssueId { get; init; }
    public Guid UserId { get; init; }
}

public enum RemoveReactionFromIssueCommandResultState
{
    IssueNotFound,
    UserNotFound,
    ReactionNotFound,
    IssueUserReactionNotFound,
    ReactionFromIssueRemoved
}