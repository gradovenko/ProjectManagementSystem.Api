using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.User.CreateProjectIssues
{
    public interface IIssuePriorityRepository
    {
        Task<IssuePriority> Get(Guid id, CancellationToken cancellationToken);
    }
}