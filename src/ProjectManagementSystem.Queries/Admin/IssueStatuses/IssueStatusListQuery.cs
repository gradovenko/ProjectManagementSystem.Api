namespace ProjectManagementSystem.Queries.Admin.IssueStatuses;

public sealed record IssueStatusListQuery(int Offset, int Limit) : PageQuery<IssueStatusListItemView>(Offset, Limit);