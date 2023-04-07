namespace ProjectManagementSystem.Queries.TimeEntries;

public sealed record TimeEntryListQuery(int Offset, int Limit) : PageQuery<TimeEntryListItemView>(Offset, Limit);