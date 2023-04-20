using MediatR;

namespace ProjectManagementSystem.Domain.TimeEntries.Commands;

public sealed record DeleteTimeEntryFromIssueCommand : IRequest<DeleteTimeEntryFromIssueCommandResultState>
{
    public Guid TimeEntryId { get; init; }
    public Guid IssueId { get; init; }
    public Guid UserId { get; init; }
}

public enum DeleteTimeEntryFromIssueCommandResultState
{
    IssueNotFound,
    UserNotFound,
    TimeEntryNotFound,
    TimeEntryFromEntryRemoved
}