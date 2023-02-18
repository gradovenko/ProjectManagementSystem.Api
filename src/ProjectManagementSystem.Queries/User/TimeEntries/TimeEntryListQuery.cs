namespace ProjectManagementSystem.Queries.User.TimeEntries;

public sealed record TimeEntryListQuery(int Offset, int Limit) : PageQuery<TimeEntryListItemView>(Offset, Limit);