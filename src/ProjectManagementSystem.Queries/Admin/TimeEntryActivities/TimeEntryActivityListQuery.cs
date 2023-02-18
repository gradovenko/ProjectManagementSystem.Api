namespace ProjectManagementSystem.Queries.Admin.TimeEntryActivities;

public sealed record TimeEntryActivityListQuery(int Offset, int Limit) : PageQuery<TimeEntryActivityListItemView>(Offset, Limit);