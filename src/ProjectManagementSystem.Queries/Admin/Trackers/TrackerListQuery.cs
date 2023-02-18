namespace ProjectManagementSystem.Queries.Admin.Trackers;

public sealed record TrackerListQuery(int Offset, int Limit) : PageQuery<TrackerListItemView>(Offset, Limit);