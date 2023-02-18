namespace ProjectManagementSystem.Domain.Admin.TimeEntryActivities;

public interface ITimeEntryActivityRepository
{
    Task<TimeEntryActivity> Get(Guid id, CancellationToken cancellationToken);

    Task Save(TimeEntryActivity timeEntryActivity);
}