using MediatR;

namespace ProjectManagementSystem.Queries.User.Accounts;

public sealed record UserQuery(Guid Id) : IRequest<UserView>;