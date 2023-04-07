namespace ProjectManagementSystem.Queries.Issues;

public sealed record IssueListQuery(int Offset, int Limit) : PageQuery<IssueListItemViewModel>(Offset, Limit);