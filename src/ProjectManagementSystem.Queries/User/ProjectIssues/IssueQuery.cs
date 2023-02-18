using MediatR;

namespace ProjectManagementSystem.Queries.User.ProjectIssues;

public sealed record IssueQuery(Guid ProjectId, Guid IssueId) : IRequest<IssueView>;