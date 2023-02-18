namespace ProjectManagementSystem.Domain.User.Issues;

public interface IProjectRepository
{
    Task<Project> Get(Guid id, CancellationToken cancellationToken);

    Task Save(Project project);
}