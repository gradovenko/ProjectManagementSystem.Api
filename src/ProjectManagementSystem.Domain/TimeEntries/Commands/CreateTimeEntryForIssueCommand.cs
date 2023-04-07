using MediatR;

namespace ProjectManagementSystem.Domain.TimeEntries.Commands;

public sealed record CreateTimeEntryForIssueCommand : IRequest<CreateTimeEntryForIssueCommandResultState>
{
    public Guid TimeEntryId { get; init; }
    public decimal Hours { get; init; }
    public string? Description { get; init; }
    public DateTime? DueDate { get; init; }
    public Guid IssueId { get; init; }
    public Guid UserId { get; init; }
}

public enum CreateTimeEntryForIssueCommandResultState
{
    IssueNotFound,
    UserNotFound,
    TimeEntryWithSameIdAlreadyExists,
    TimeEntryForIssueCreated
}