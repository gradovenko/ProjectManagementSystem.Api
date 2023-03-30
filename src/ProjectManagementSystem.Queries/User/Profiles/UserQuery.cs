using MediatR;

namespace ProjectManagementSystem.Queries.User.Profiles;

public sealed record UserQuery(Guid Id) : IRequest<UserViewModel>;