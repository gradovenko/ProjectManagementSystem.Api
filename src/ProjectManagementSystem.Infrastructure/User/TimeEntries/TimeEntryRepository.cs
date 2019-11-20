using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagementSystem.Domain.User.TimeEntries;

namespace ProjectManagementSystem.Infrastructure.User.TimeEntries
{
    public sealed class TimeEntryRepository : ITimeEntryRepository

    {
        public Task<TimeEntry> Get(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Save(TimeEntry timeEntry)
        {
            throw new NotImplementedException();
        }
    }
}