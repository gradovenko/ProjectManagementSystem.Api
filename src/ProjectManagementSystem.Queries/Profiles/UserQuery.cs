using MediatR;

namespace ProjectManagementSystem.Queries.Profiles;

public sealed record UserQuery(Guid Id) : IRequest<UserViewModel>;