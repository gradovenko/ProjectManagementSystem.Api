using MediatR;

namespace ProjectManagementSystem.Domain.Labels.Commands;

public sealed record UpdateLabelCommand : IRequest<UpdateLabelCommandResultState>
{
    public Guid LabelId { get; init; }
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public string BackgroundColor { get; init; } = null!;
}

public enum UpdateLabelCommandResultState
{
    LabelNotFound,
    LabelWithSameTitleAlreadyExists,
    LabelUpdated
}