namespace ProjectManagementSystem.Queries.User.ProjectIssues;

public sealed record ProjectIssueListQuery(Guid ProjectId, int Offset, int Limit) : PageQuery<ProjectIssueListItemViewModel>(Offset, Limit);