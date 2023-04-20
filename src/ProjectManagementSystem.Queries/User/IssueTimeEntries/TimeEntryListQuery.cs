using MediatR;

namespace ProjectManagementSystem.Queries.User.IssueTimeEntries;

public sealed record TimeEntryListQuery(Guid IssueId) : IRequest<IEnumerable<TimeEntryListItemViewModel>>;