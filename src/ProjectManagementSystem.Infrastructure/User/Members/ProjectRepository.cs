using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.User.Members;

namespace ProjectManagementSystem.Infrastructure.User.Members
{
    public sealed class ProjectRepository : IProjectRepository
    {
        private readonly MemberDbContext _context;

        public ProjectRepository(MemberDbContext context)
        {
            _context = context;
        }

        public async Task<Project> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Projects.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
        }
    }
}