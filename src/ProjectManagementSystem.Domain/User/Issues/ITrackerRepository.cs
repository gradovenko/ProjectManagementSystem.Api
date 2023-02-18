namespace ProjectManagementSystem.Domain.User.Issues;

public interface ITrackerRepository
{
    Task<Tracker> Get(Guid id, CancellationToken cancellationToken);
}