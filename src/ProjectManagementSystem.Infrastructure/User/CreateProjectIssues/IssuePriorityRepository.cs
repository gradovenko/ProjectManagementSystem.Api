using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.User.CreateProjectIssues;

namespace ProjectManagementSystem.Infrastructure.User.CreateProjectIssues
{
    public sealed class IssuePriorityRepository : IIssuePriorityRepository
    {
        private readonly ProjectDbContext _context;

        public IssuePriorityRepository(ProjectDbContext context)
        {
            _context = context;
        }
        
        public async Task<IssuePriority> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.IssuePriorities
                .AsNoTracking()
                .SingleOrDefaultAsync(ip => ip.Id == id, cancellationToken);
        }
    }
}