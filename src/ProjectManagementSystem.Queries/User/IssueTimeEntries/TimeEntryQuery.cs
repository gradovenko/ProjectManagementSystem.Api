using MediatR;

namespace ProjectManagementSystem.Queries.User.IssueTimeEntries;

public sealed record TimeEntryQuery(Guid IssueId, Guid TimeEntryId) : IRequest<TimeEntryView>;