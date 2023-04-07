using MediatR;
using ProjectManagementSystem.Domain.Projects;
using ProjectManagementSystem.Domain.Projects.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Projects;

public sealed class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, UpdateProjectCommandResultState>
{
    private readonly IProjectRepository _projectRepository;

    public UpdateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
    }

    public async Task<UpdateProjectCommandResultState> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        Project? project = await _projectRepository.Get(request.ProjectId, cancellationToken);

        if (project == null)
            return UpdateProjectCommandResultState.ProjectNotFound;

        if (request.Name != project.Name)
        {
            Project? projectWithSameName = await _projectRepository.GetByName(request.Name, cancellationToken);

            if (projectWithSameName != null)
                return UpdateProjectCommandResultState.ProjectWithSameNameAlreadyExists;
        }

        if (request.Path != project.Path)
        {
            Project? projectWithSamePath = await _projectRepository.GetByPath(request.Path, cancellationToken);

            if (projectWithSamePath != null)
                return UpdateProjectCommandResultState.ProjectWithSamePathAlreadyExists;
        }

        project.Update(request.Name, request.Description, request.Path, request.Visibility);

        await _projectRepository.Save(project, cancellationToken);

        return UpdateProjectCommandResultState.ProjectUpdated;
    }
}