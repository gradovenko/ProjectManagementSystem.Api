namespace ProjectManagementSystem.Domain.Admin.Projects;

public interface ITrackerRepository
{
    Task<Tracker> Get(Guid id, CancellationToken cancellationToken);
}