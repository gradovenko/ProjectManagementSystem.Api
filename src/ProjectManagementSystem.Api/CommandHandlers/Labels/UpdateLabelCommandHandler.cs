using MediatR;
using ProjectManagementSystem.Domain.Labels;
using ProjectManagementSystem.Domain.Labels.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Labels;

public sealed class UpdateLabelCommandHandler : IRequestHandler<UpdateProjectLabelCommand, UpdateProjectLabelCommandResultState>
{
    private readonly ILabelRepository _labelRepository;
    private readonly IProjectGetter _projectGetter;

    public UpdateLabelCommandHandler(ILabelRepository labelRepository, IProjectGetter projectGetter)
    {
        _labelRepository = labelRepository ?? throw new ArgumentNullException(nameof(labelRepository));
        _projectGetter = projectGetter ?? throw new ArgumentNullException(nameof(projectGetter));
    }

    public async Task<UpdateProjectLabelCommandResultState> Handle(UpdateProjectLabelCommand request, CancellationToken cancellationToken)
    {
        Project? project = await _projectGetter.Get(request.ProjectId, cancellationToken);

        if (project == null)
            return UpdateProjectLabelCommandResultState.ProjectNotFound;

        Label? label = await _labelRepository.Get(request.LabelId, cancellationToken);

        if (label == null)
            return UpdateProjectLabelCommandResultState.LabelNotFound;

        if (request.Title == label.Title)
            return UpdateProjectLabelCommandResultState.LabelWithSameTitleAlreadyExists;

        label.Update(request.Title, request.Description, request.BackgroundColor);

        await _labelRepository.Save(label, cancellationToken);

        return UpdateProjectLabelCommandResultState.LabelUpdated;
    }
}