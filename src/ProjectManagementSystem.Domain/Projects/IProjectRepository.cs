namespace ProjectManagementSystem.Domain.Projects;

public interface IProjectRepository
{
    Task<Project?> Get(Guid id, CancellationToken cancellationToken);

    Task Save(Project project, CancellationToken cancellationToken);
}