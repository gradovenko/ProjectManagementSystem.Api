using MediatR;

namespace ProjectManagementSystem.Domain.Issues.Commands;

public sealed record AddReactionToIssueCommand : IRequest<AddReactionToIssueCommandResultState>
{
    public string ReactionId { get; init; } = null!;
    public Guid IssueId { get; init; }
    public Guid UserId { get; init; }
}

public enum AddReactionToIssueCommandResultState
{
    IssueNotFound,
    UserNotFound,
    ReactionNotFound,
    ReactionToIssueAdded
}