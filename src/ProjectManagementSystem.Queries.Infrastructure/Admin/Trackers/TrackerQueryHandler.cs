using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.Trackers;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.Trackers;

public sealed class TrackerQueryHandler : IRequestHandler<TrackerQuery, TrackerView>
{
    private readonly TrackerDbContext _context;

    public TrackerQueryHandler(TrackerDbContext context)
    {
        _context = context;
    }

    public async Task<TrackerView> Handle(TrackerQuery query, CancellationToken cancellationToken)
    {
        return await _context.Trackers.AsNoTracking()
            .Where(project => project.Id == query.Id)
            .Select(project => new TrackerView
            {
                Name = project.Name,
            })
            .SingleOrDefaultAsync(cancellationToken);
    }
}