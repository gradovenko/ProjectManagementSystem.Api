using MediatR;

namespace ProjectManagementSystem.Domain.Projects.Commands;

public sealed record UpdateProjectCommand : IRequest<UpdateProjectCommandResultState>
{
    public Guid ProjectId { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public string Path { get; init; } = null!;
    public ProjectVisibility Visibility { get; init; }
}

public enum UpdateProjectCommandResultState
{
    ProjectNotFound,
    ProjectWithSameNameAlreadyExists,
    ProjectWithSamePathAlreadyExists,
    ProjectUpdated
}