using MediatR;

namespace ProjectManagementSystem.Domain.Labels.Commands;

public sealed record CreateProjectLabelCommand : IRequest<CreateProjectLabelCommandResultState>
{
    public Guid LabelId { get; init; }
    public Guid ProjectId { get; init; }
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public string BackgroundColor { get; init; } = null!;
}

public enum CreateProjectLabelCommandResultState
{
    ProjectNotFound,
    LabelWithSameTitleAlreadyExists,
    LabelWithSameIdButOtherParamsAlreadyExists,
    LabelCreated
}