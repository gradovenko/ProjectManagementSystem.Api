using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.User.CreateProjectIssues
{
    public interface ITrackerRepository
    {
        Task<Tracker> Get(Guid id, CancellationToken cancellationToken);
    }
}