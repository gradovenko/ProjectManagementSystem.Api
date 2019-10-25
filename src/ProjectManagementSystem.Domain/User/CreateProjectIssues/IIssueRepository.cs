using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.User.CreateProjectIssues
{
    public interface IIssueRepository
    {
        Task<Issue> Get(Guid id, CancellationToken cancellationToken);
    }
}