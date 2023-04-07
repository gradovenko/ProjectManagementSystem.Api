namespace ProjectManagementSystem.Domain.TimeEntries;

public interface ITimeEntryRepository
{
    Task<TimeEntry?> Get(Guid id, CancellationToken cancellationToken);
    Task Save(TimeEntry timeEntry, CancellationToken cancellationToken);
}