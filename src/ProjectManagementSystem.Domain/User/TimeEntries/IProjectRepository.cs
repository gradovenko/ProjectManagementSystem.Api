using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.User.TimeEntries
{
    public interface IProjectRepository
    {
        Task<Project> Get(Guid id, CancellationToken cancellationToken);
    }
}