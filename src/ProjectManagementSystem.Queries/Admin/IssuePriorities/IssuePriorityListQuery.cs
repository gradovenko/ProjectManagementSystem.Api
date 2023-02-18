namespace ProjectManagementSystem.Queries.Admin.IssuePriorities;

public sealed record IssuePriorityListQuery(int Offset, int Limit) : PageQuery<IssuePriorityListItemView>(Offset, Limit);