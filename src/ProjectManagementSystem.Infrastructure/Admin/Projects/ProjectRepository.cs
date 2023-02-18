using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Admin.Projects;

namespace ProjectManagementSystem.Infrastructure.Admin.Projects;

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
            .AsNoTracking()
            .SingleOrDefaultAsync(ip => ip.Id == id, cancellationToken);
    }

    public async Task Save(Project project)
    {
        if (_context.Entry(project).State == EntityState.Detached)
            await _context.Projects.AddAsync(project);

        await _context.SaveChangesAsync(); 
    }
}