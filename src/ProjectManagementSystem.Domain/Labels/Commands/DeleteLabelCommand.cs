using MediatR;

namespace ProjectManagementSystem.Domain.Labels.Commands;

public sealed record DeleteLabelCommand : IRequest<DeleteLabelCommandResultState>
{
    public Guid LabelId { get; init; }
}

public enum DeleteLabelCommandResultState
{
    LabelNotFound,
    LabelDeleted
}