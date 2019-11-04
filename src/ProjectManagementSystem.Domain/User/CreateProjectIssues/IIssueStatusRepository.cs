using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.User.CreateProjectIssues
{
    public interface IIssueStatusRepository
    {
        Task<IssueStatus> Get(Guid id, CancellationToken cancellationToken);
    }
}