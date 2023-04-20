namespace ProjectManagementSystem.Queries.User.Issues;

public sealed record IssueListQuery(int Offset, int Limit) : PageQuery<IssueListItemViewModel>(Offset, Limit);