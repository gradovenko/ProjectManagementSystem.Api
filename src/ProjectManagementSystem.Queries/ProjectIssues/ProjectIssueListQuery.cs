namespace ProjectManagementSystem.Queries.ProjectIssues;

public sealed record ProjectIssueListQuery(Guid ProjectId, int Offset, int Limit) : PageQuery<ProjectIssueListItemViewModel>(Offset, Limit);