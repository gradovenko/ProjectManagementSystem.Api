using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Labels;
using ProjectManagementSystem.Infrastructure.Issues;

namespace ProjectManagementSystem.Infrastructure.Labels;

public sealed class ProjectGetter : IProjectGetter
{
    private readonly LabelDbContext _context;

    public ProjectGetter(LabelDbContext context)
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