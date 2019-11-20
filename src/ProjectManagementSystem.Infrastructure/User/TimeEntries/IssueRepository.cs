using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagementSystem.Domain.User.TimeEntries;

namespace ProjectManagementSystem.Infrastructure.User.TimeEntries
{
    public sealed class IssueRepository : IIssueRepository
    {
        private readonly TimeEntryDbContext _context;

        public IssueRepository(TimeEntryDbContext context)
        {
            _context = context;
        }
        
        public Task<Issue> Get(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Save(Issue project)
        {
            throw new NotImplementedException();
        }
    }
}