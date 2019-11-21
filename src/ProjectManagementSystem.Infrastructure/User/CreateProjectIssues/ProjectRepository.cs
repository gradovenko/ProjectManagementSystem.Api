using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.User.CreateProjectIssues;

namespace ProjectManagementSystem.Infrastructure.User.CreateProjectIssues
{
    public sealed class ProjectRepository : IProjectRepository
    {
        private readonly ProjectDbContext _context;

        public ProjectRepository(ProjectDbContext context)
        {
            _context = context;
        }

        public async Task<Project> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Projects
                .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task Save(Project project)
        {
            if (_context.Entry(project).State == EntityState.Detached)
                _context.Projects.Add(project);

            await _context.SaveChangesAsync(); 
        }
    }
}