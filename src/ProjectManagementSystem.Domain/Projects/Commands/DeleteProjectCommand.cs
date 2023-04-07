using MediatR;

namespace ProjectManagementSystem.Domain.Projects.Commands;

public sealed record DeleteProjectCommand : IRequest<DeleteProjectCommandResultState>
{
    public Guid ProjectId { get; init; }
}

public enum DeleteProjectCommandResultState
{
    ProjectDeleted,
    ProjectNotFound
}