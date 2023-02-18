using MediatR;

namespace ProjectManagementSystem.Queries.Admin.Projects;

public sealed record ProjectQuery(Guid Id) : IRequest<ProjectView>;