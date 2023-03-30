using MediatR;

namespace ProjectManagementSystem.Domain.Labels.Commands;

public sealed record DeleteProjectLabelCommand : IRequest<DeleteProjectLabelCommandResultState>
{
    public Guid LabelId { get; init; }
    public Guid ProjectId { get; init; }
}

public enum DeleteProjectLabelCommandResultState
{
    ProjectNotFound,
    LabelNotFound,
    LabelDeleted
}