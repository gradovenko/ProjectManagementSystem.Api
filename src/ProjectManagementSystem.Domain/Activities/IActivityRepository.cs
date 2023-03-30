namespace ProjectManagementSystem.Domain.Activities;

public interface IActivityRepository
{
    Task<Activity?> Get(Guid id, CancellationToken cancellationToken);
    Task Save(Activity activity, CancellationToken cancellationToken);
}