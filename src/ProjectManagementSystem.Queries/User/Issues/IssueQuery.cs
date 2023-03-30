using MediatR;

namespace ProjectManagementSystem.Queries.User.Issues;

public sealed record IssueQuery(Guid Id) : IRequest<IssueViewModel?>;