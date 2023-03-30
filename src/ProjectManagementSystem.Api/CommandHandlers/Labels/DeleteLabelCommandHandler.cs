using MediatR;
using ProjectManagementSystem.Domain.Labels;
using ProjectManagementSystem.Domain.Labels.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Labels;

public sealed class DeleteLabelCommandHandler : IRequestHandler<DeleteProjectLabelCommand, DeleteProjectLabelCommandResultState>
{
    private readonly ILabelRepository _labelRepository;
    private readonly IProjectGetter _projectGetter;

    public DeleteLabelCommandHandler(ILabelRepository labelRepository, IProjectGetter projectGetter)
    {
        _labelRepository = labelRepository ?? throw new ArgumentNullException(nameof(labelRepository));
        _projectGetter = projectGetter ?? throw new ArgumentNullException(nameof(projectGetter));
    }

    public async Task<DeleteProjectLabelCommandResultState> Handle(DeleteProjectLabelCommand request, CancellationToken cancellationToken)
    {
        Project? project = await _projectGetter.Get(request.ProjectId, cancellationToken);

        if (project == null)
            return DeleteProjectLabelCommandResultState.ProjectNotFound;

        Label? label = await _labelRepository.Get(request.LabelId, cancellationToken);

        if (label == null)
            return DeleteProjectLabelCommandResultState.LabelNotFound;

        label.Delete();

        await _labelRepository.Save(label, cancellationToken);

        return DeleteProjectLabelCommandResultState.LabelDeleted;
    }
}