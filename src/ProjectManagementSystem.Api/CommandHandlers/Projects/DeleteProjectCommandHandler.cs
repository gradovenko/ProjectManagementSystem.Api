using MediatR;
using ProjectManagementSystem.Domain.Projects;
using ProjectManagementSystem.Domain.Projects.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Projects;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, DeleteProjectCommandResultState>
{
    private readonly IProjectRepository _projectRepository;

    public DeleteProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
    }

    public async Task<DeleteProjectCommandResultState> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        Project? project = await _projectRepository.Get(request.ProjectId, cancellationToken);

        if (project == null)
            return DeleteProjectCommandResultState.ProjectNotFound;

        project.Delete();

        await _projectRepository.Save(project, cancellationToken);

        return DeleteProjectCommandResultState.ProjectDeleted;
    }
}