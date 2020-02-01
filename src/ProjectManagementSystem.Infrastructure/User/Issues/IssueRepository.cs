using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.User.Issues;

namespace ProjectManagementSystem.Infrastructure.User.Issues
{
    public sealed class IssueRepository : IIssueRepository
    {
        private readonly ProjectDbContext _context;

        public IssueRepository(ProjectDbContext context)
        {
            _context = context;
        }

        public async Task<Issue> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Issues
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
        }
    }
}