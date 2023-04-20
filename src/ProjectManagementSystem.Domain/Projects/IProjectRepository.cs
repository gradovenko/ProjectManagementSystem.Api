namespace ProjectManagementSystem.Domain.Projects;

public interface IProjectRepository
{
    Task<Project?> Get(Guid id, CancellationToken cancellationToken);
    Task<Project?> GetByName(string name, CancellationToken cancellationToken);
    Task<Project?> GetByPath(string path, CancellationToken cancellationToken);
    Task Save(Project project, CancellationToken cancellationToken);
}