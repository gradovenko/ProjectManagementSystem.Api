using MediatR;

namespace ProjectManagementSystem.Queries.ProjectIssues;

public sealed record ProjectIssueQuery(Guid ProjectId, Guid IssueId) : IRequest<ProjectIssueViewModel?>;