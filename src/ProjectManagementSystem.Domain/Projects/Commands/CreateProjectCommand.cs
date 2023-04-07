using MediatR;

namespace ProjectManagementSystem.Domain.Projects.Commands;

public sealed record CreateProjectCommand : IRequest<CreateProjectCommandResultState>
{
    public Guid ProjectId { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public string Path { get; init; } = null!;
    public ProjectVisibility Visibility { get; init; }
}

public enum CreateProjectCommandResultState
{
    ProjectWithSameNameAlreadyExists,
    ProjectWithSamePathAlreadyExists,
    ProjectWithSameIdButOtherParamsAlreadyExists,
    ProjectCreated
}