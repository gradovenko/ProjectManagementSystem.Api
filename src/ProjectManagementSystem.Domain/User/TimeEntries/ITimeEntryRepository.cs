using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.User.TimeEntries
{
    public interface ITimeEntryRepository
    {
        Task<TimeEntry> Get(Guid id, CancellationToken cancellationToken);

        Task Save(TimeEntry timeEntry);
    }
}