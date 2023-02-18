namespace ProjectManagementSystem.Queries.User.ProjectIssues;

public sealed record IssueListQuery(Guid ProjectId, int Offset, int Limit) : PageQuery<IssueListItemView>(Offset, Limit);