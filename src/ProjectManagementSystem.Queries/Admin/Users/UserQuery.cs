using MediatR;

namespace ProjectManagementSystem.Queries.Admin.Users;

public sealed record UserQuery(Guid Id) : IRequest<UserView>;