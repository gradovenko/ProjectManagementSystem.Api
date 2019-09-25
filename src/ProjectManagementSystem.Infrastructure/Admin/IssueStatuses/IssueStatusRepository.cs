using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagementSystem.Domain.Admin.IssueStatuses;

namespace ProjectManagementSystem.Infrastructure.Admin.IssueStatuses
{
    public class IssueStatusRepository : IIssueStatusRepository
    {
        public Task<IssueStatus> Get(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Save(IssueStatus issuePriority)
        {
            throw new NotImplementedException();
        }
    }
}