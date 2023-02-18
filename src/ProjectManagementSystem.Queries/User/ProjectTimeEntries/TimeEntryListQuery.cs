namespace ProjectManagementSystem.Queries.User.ProjectTimeEntries;

public sealed record TimeEntryListQuery(Guid ProjectId, int Offset, int Limit) : PageQuery<TimeEntryListItemView>(Offset, Limit);