using MediatR;

namespace ProjectManagementSystem.Queries.Admin.IssueStatuses;

public sealed record IssueStatusQuery(Guid Id) : IRequest<IssueStatusView>;