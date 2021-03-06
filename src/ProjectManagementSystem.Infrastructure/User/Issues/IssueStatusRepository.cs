using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.User.Issues;

namespace ProjectManagementSystem.Infrastructure.User.Issues
{
    public sealed class IssueStatusRepository : IIssueStatusRepository
    {
        private readonly ProjectDbContext _context;

        public IssueStatusRepository(ProjectDbContext context)
        {
            _context = context;
        }
        
        public async Task<IssueStatus> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.IssueStatuses
                .AsNoTracking()
                .SingleOrDefaultAsync(ip => ip.Id == id, cancellationToken);
        }
    }
}