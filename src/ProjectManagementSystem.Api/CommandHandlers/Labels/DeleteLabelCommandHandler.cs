using MediatR;
using ProjectManagementSystem.Domain.Labels;
using ProjectManagementSystem.Domain.Labels.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Labels;

public sealed class DeleteLabelCommandHandler : IRequestHandler<DeleteLabelCommand, DeleteLabelCommandResultState>
{
    private readonly ILabelRepository _labelRepository;

    public DeleteLabelCommandHandler(ILabelRepository labelRepository)
    {
        _labelRepository = labelRepository ?? throw new ArgumentNullException(nameof(labelRepository));
    }

    public async Task<DeleteLabelCommandResultState> Handle(DeleteLabelCommand request, CancellationToken cancellationToken)
    {
        Label? label = await _labelRepository.Get(request.LabelId, cancellationToken);

        if (label == null)
            return DeleteLabelCommandResultState.LabelNotFound;

        label.Delete();

        await _labelRepository.Save(label, cancellationToken);

        return DeleteLabelCommandResultState.LabelDeleted;
    }
}