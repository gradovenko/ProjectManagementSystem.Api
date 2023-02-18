namespace ProjectManagementSystem.Domain.User.TimeEntries;

public interface ITimeEntryActivityRepository
{
    Task<TimeEntryActivity> Get(Guid id, CancellationToken cancellationToken);
}