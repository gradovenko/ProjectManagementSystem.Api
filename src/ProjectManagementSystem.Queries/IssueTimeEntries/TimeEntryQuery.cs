using MediatR;

namespace ProjectManagementSystem.Queries.IssueTimeEntries;

public sealed record TimeEntryQuery(Guid IssueId, Guid TimeEntryId) : IRequest<TimeEntryView>;