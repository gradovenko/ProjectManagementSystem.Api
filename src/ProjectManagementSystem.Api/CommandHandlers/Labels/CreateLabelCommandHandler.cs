using MediatR;
using ProjectManagementSystem.Domain.Labels;
using ProjectManagementSystem.Domain.Labels.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Labels;

public sealed class CreateLabelCommandHandler : IRequestHandler<CreateProjectLabelCommand, CreateProjectLabelCommandResultState>
{
    private readonly ILabelRepository _labelRepository;
    private readonly IProjectGetter _projectGetter;

    public CreateLabelCommandHandler(ILabelRepository labelRepository, IProjectGetter projectGetter)
    {
        _labelRepository = labelRepository ?? throw new ArgumentNullException(nameof(labelRepository));
        _projectGetter = projectGetter ?? throw new ArgumentNullException(nameof(projectGetter));
    }

    public async Task<CreateProjectLabelCommandResultState> Handle(CreateProjectLabelCommand request, CancellationToken cancellationToken)
    {
        Project? project = await _projectGetter.Get(request.ProjectId, cancellationToken);

        if (project == null)
            return CreateProjectLabelCommandResultState.ProjectNotFound;

        Label? label = await _labelRepository.Get(request.LabelId, cancellationToken);

        if (label != null)
        {
            if (request.Title == label.Title)
                return CreateProjectLabelCommandResultState.LabelWithSameTitleAlreadyExists;
            return CreateProjectLabelCommandResultState.LabelWithSameIdButOtherParamsAlreadyExists;
        }

        label = new Label(request.LabelId, request.Title, request.Description, request.BackgroundColor);

        await _labelRepository.Save(label, cancellationToken);

        return CreateProjectLabelCommandResultState.LabelCreated;
    }
}