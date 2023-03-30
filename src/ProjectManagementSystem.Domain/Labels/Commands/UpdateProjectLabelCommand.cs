using MediatR;

namespace ProjectManagementSystem.Domain.Labels.Commands;

public sealed record UpdateProjectLabelCommand : IRequest<UpdateProjectLabelCommandResultState>
{
    public Guid LabelId { get; init; }
    public Guid ProjectId { get; init; }
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public string BackgroundColor { get; init; } = null!;
}

public enum UpdateProjectLabelCommandResultState
{
    ProjectNotFound,
    LabelNotFound,
    LabelWithSameTitleAlreadyExists,
    LabelUpdated
}