namespace ProjectManagementSystem.Domain.User.TimeEntries;

public interface ITimeEntryRepository
{
    Task<TimeEntry> Get(Guid id, CancellationToken cancellationToken);
}