using MediatR;
using ProjectManagementSystem.Domain.Projects;
using ProjectManagementSystem.Domain.Projects.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Projects;

public sealed class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, CreateProjectCommandResultState>
{
    private readonly IProjectRepository _projectRepository;

    public CreateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
    }

    public async Task<CreateProjectCommandResultState> Handle(CreateProjectCommand request,
        CancellationToken cancellationToken)
    {
        Project? project = await _projectRepository.Get(request.ProjectId, cancellationToken);

        if (project != null)
        {
            if (project.Name == request.Name)
                return CreateProjectCommandResultState.ProjectWithSameNameAlreadyExists;
            if (project.Path == request.Path)
                return CreateProjectCommandResultState.ProjectWithSamePathAlreadyExists;
            return CreateProjectCommandResultState.ProjectWithSameIdButOtherParamsAlreadyExists;
        }

        await _projectRepository.Save(
            new Project(request.ProjectId, request.Name, request.Description, request.Path, request.Visibility),
            cancellationToken);

        return CreateProjectCommandResultState.ProjectCreated;
    }
}