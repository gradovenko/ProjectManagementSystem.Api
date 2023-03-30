namespace ProjectManagementSystem.Domain.Labels;

public interface IProjectGetter
{
    Task<Project?> Get(Guid id, CancellationToken cancellationToken);
}