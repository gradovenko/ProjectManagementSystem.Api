using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Projects;

namespace ProjectManagementSystem.Infrastructure.Projects;

public sealed class ProjectRepository : IProjectRepository
{
    private readonly ProjectDbContext _context;

    public ProjectRepository(ProjectDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Project?> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Projects
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Project?> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _context.Projects.AsNoTracking()
            .SingleOrDefaultAsync(p => p.Name == name, cancellationToken);
    }

    public async Task<Project?> GetByPath(string path, CancellationToken cancellationToken)
    {
        return await _context.Projects.AsNoTracking()
            .SingleOrDefaultAsync(p => p.Path == path, cancellationToken);
    }

    public async Task Save(Project project, CancellationToken cancellationToken)
    {
        if (_context.Entry(project).State == EntityState.Detached)
            await _context.Projects.AddAsync(project, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken); 
    }
}