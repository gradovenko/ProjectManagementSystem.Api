using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Labels;

namespace ProjectManagementSystem.Infrastructure.Labels;

public sealed class LabelRepository : ILabelRepository
{
    private readonly LabelDbContext _context;

    public LabelRepository(LabelDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Label?> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Labels
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task Save(Label label, CancellationToken cancellationToken)
    {
        if (_context.Entry(label).State == EntityState.Detached)
            await _context.Labels.AddAsync(label, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken); 
    }
}