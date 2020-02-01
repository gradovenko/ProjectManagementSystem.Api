using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.Admin.Trackers
{
    public interface ITrackerRepository
    {
        Task<Tracker> Get(Guid id, CancellationToken cancellationToken);

        Task Save(Tracker tracker);
    }
}