using MediatR;

namespace ProjectManagementSystem.Domain.Issues.Commands;

public sealed class UpdateIssueCommand : IRequest<UpdateIssueCommandResultState>
{
    public Guid IssueId { get; init; }
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public Guid UserId { get; init; }
}

public enum UpdateIssueCommandResultState
{
    UserNotFound,
    IssueNotFound,
    IssueUpdated
}