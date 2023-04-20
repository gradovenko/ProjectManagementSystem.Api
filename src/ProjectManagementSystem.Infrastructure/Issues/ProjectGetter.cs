using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Issues;

namespace ProjectManagementSystem.Infrastructure.Issues;

public sealed class ProjectGetter : IProjectGetter
{
    private readonly IssueDbContext _context;

    public ProjectGetter(IssueDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Project?> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Projects
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}