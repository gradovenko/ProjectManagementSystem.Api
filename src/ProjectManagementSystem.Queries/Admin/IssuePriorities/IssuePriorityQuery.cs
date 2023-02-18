using MediatR;

namespace ProjectManagementSystem.Queries.Admin.IssuePriorities;

public sealed record IssuePriorityQuery(Guid Id) : IRequest<IssuePriorityView>;