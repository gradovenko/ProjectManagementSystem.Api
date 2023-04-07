namespace ProjectManagementSystem.Domain.Issues;

public interface IProjectGetter
{
    Task<Project?> Get(Guid id, CancellationToken cancellationToken);
}