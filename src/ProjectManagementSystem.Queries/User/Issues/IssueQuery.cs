using MediatR;

namespace ProjectManagementSystem.Queries.User.Issues;

public sealed record IssueQuery(Guid IssueId) : IRequest<IssueViewModel?>;