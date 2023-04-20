using MediatR;

namespace ProjectManagementSystem.Queries.User.Projects;

public sealed record ProjectQuery(Guid Id) : IRequest<ProjectViewModel?>;