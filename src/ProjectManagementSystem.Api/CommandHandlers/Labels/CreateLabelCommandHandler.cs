using MediatR;
using ProjectManagementSystem.Domain.Labels;
using ProjectManagementSystem.Domain.Labels.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Labels;

public sealed class CreateLabelCommandHandler : IRequestHandler<CreateLabelCommand, CreateLabelCommandResultState>
{
    private readonly ILabelRepository _labelRepository;

    public CreateLabelCommandHandler(ILabelRepository labelRepository)
    {
        _labelRepository = labelRepository ?? throw new ArgumentNullException(nameof(labelRepository));
    }

    public async Task<CreateLabelCommandResultState> Handle(CreateLabelCommand request, CancellationToken cancellationToken)
    {
        Label? label = await _labelRepository.Get(request.LabelId, cancellationToken);

        if (label != null)
        {
            if (request.Title == label.Title)
                return CreateLabelCommandResultState.LabelWithSameTitleAlreadyExists;
            return CreateLabelCommandResultState.LabelWithSameIdButOtherParamsAlreadyExists;
        }

        label = new Label(request.LabelId, request.Title, request.Description, request.BackgroundColor);

        await _labelRepository.Save(label, cancellationToken);

        return CreateLabelCommandResultState.LabelCreated;
    }
}