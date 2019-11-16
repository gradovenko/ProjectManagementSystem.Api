using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagementSystem.Domain.Admin.TimeEntryActivities;

namespace ProjectManagementSystem.Infrastructure.Admin.TimeEntryActivities
{
    public sealed class TimeEntryActivityRepository : ITimeEntryActivityRepository
    {
        public Task<TimeEntryActivity> Get(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Save(TimeEntryActivity timeEntryActivity)
        {
            throw new NotImplementedException();
        }
    }
}