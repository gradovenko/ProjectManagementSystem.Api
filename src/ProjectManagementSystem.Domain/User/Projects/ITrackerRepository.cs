namespace ProjectManagementSystem.Domain.User.Projects;

public interface ITrackerRepository
{
    Task<Tracker> Get(Guid id, CancellationToken cancellationToken);
}