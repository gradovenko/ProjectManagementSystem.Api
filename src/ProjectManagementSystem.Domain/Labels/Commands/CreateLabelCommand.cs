using MediatR;

namespace ProjectManagementSystem.Domain.Labels.Commands;

public sealed record CreateLabelCommand : IRequest<CreateLabelCommandResultState>
{
    public Guid LabelId { get; init; }
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public string BackgroundColor { get; init; } = null!;
}

public enum CreateLabelCommandResultState
{
    LabelWithSameTitleAlreadyExists,
    LabelWithSameIdButOtherParamsAlreadyExists,
    LabelCreated
}