namespace ProjectManagementSystem.Queries.User.IssueTimeEntries;

public sealed record TimeEntryListQuery(Guid Id, int Offset, int Limit) : PageQuery<TimeEntryListItemView>(Offset, Limit);