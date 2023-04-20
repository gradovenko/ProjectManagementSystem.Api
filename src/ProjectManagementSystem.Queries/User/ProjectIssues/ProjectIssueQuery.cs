using MediatR;

namespace ProjectManagementSystem.Queries.User.ProjectIssues;

public sealed record ProjectIssueQuery(Guid ProjectId, Guid IssueId) : IRequest<ProjectIssueViewModel?>;