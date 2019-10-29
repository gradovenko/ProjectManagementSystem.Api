using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagementSystem.Domain.User.CreateProjectIssues;

namespace ProjectManagementSystem.Infrastructure.User.CreateProjectIssues
{
    public sealed class IssueRepository : IIssueRepository
    {
        private readonly ProjectDbContext _context;

        public IssueRepository(ProjectDbContext context)
        {
            _context = context;
        }

        public Task<Issue> Get(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}