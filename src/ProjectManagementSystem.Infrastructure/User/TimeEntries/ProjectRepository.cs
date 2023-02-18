using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.User.TimeEntries;

namespace ProjectManagementSystem.Infrastructure.User.TimeEntries;

public class ProjectRepository : IProjectRepository
{
    private readonly IssueDbContext _context;

    public ProjectRepository(IssueDbContext context)
    {
        _context = context;
    }
        
    public async Task<Project> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Projects
            .AsNoTracking()
            .SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
}