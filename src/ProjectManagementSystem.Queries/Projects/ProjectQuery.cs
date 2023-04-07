using MediatR;

namespace ProjectManagementSystem.Queries.Projects;

public sealed record ProjectQuery(Guid Id) : IRequest<ProjectViewModel?>;