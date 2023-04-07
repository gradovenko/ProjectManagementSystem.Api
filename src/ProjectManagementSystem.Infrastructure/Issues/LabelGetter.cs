using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Issues;

namespace ProjectManagementSystem.Infrastructure.Issues;

public sealed class LabelGetter : ILabelGetter
{
    private readonly IssueDbContext _context;

    public LabelGetter(IssueDbContext context)
    {
        _context = context;
    }

    public async Task<Label?> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Labels
            .AsNoTracking()
            .SingleOrDefaultAsync(l => l.Id == id, cancellationToken);
    }
}