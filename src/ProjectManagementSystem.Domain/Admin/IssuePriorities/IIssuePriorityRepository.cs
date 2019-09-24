using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.Admin.IssuePriorities
{
    public interface IIssuePriorityRepository
    {
        Task<IssuePriority> Get(Guid id, CancellationToken cancellationToken);

        Task Save(IssuePriority issuePriority);
    }
}