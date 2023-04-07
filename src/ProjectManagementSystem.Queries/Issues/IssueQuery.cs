using MediatR;

namespace ProjectManagementSystem.Queries.Issues;

public sealed record IssueQuery(Guid IssueId) : IRequest<IssueViewModel?>;