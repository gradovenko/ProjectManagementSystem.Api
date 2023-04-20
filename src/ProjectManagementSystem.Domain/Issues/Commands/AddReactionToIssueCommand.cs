using MediatR;

namespace ProjectManagementSystem.Domain.Issues.Commands;

public sealed record AddReactionToIssueCommand : IRequest<AddReactionToIssueCommandResultState>
{
    public Guid ReactionId { get; init; }
    public Guid IssueId { get; init; }
    public Guid UserId { get; init; }
}

public enum AddReactionToIssueCommandResultState
{
    IssueNotFound,
    UserNotFound,
    ReactionNotFound,
    IssueUserReactionAlreadyExists,
    ReactionToIssueAdded
}