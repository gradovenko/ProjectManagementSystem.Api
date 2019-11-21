using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.User.TimeEntries
{
    public interface ITimeEntryActivityRepository
    {
        Task<TimeEntryActivity> Get(Guid id, CancellationToken cancellationToken);
    }
}