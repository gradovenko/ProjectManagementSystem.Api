using MediatR;
using ProjectManagementSystem.Domain.Labels;
using ProjectManagementSystem.Domain.Labels.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Labels;

public sealed class UpdateLabelCommandHandler : IRequestHandler<UpdateLabelCommand, UpdateLabelCommandResultState>
{
    private readonly ILabelRepository _labelRepository;

    public UpdateLabelCommandHandler(ILabelRepository labelRepository)
    {
        _labelRepository = labelRepository ?? throw new ArgumentNullException(nameof(labelRepository));
    }

    public async Task<UpdateLabelCommandResultState> Handle(UpdateLabelCommand request, CancellationToken cancellationToken)
    {
        Label? label = await _labelRepository.Get(request.LabelId, cancellationToken);

        if (label == null)
            return UpdateLabelCommandResultState.LabelNotFound;

        if (request.Title == label.Title)
            return UpdateLabelCommandResultState.LabelWithSameTitleAlreadyExists;

        label.Update(request.Title, request.Description, request.BackgroundColor);

        await _labelRepository.Save(label, cancellationToken);

        return UpdateLabelCommandResultState.LabelUpdated;
    }
}