using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.TimeEntries;

namespace ProjectManagementSystem.Infrastructure.TimeEntries;

public sealed class UserGetter : IUserGetter
{
    private readonly TimeEntryDbContext _context;

    public UserGetter(TimeEntryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<User?> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
}