using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.User.TimeEntries;

namespace ProjectManagementSystem.Infrastructure.User.TimeEntries
{
    public sealed class IssueRepository : IIssueRepository
    {
        private readonly IssueDbContext _context;

        public IssueRepository(IssueDbContext context)
        {
            _context = context;
        }
        
        public async Task<Issue> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Issues
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task Save(Issue issue)
        {
            if (_context.Entry(issue).State == EntityState.Detached)
                _context.Issues.Add(issue);

            await _context.SaveChangesAsync();         
        }
    }
}