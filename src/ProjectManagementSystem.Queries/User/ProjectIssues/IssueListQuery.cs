namespace ProjectManagementSystem.Queries.User.ProjectIssues;

public sealed record IssueListQuery(Guid ProjectId, int Offset, int Limit) : PageQuery<IssueListItemViewModel>(Offset, Limit);