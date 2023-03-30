using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.Trackers;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.Trackers;

public sealed class TrackerListQueryHandler : IRequestHandler<TrackerListQuery, PageViewModel<TrackerListItemView>>
{
    private readonly TrackerDbContext _context;

    public TrackerListQueryHandler(TrackerDbContext context)
    {
        _context = context;
    }
        
    public async Task<PageViewModel<TrackerListItemView>> Handle(TrackerListQuery query, CancellationToken cancellationToken)
    {
        var sql = _context.Trackers.AsNoTracking()
            .Select(project => new TrackerListItemView
            {
                Id = project.Id,
                Name = project.Name
            });

        return new PageViewModel<TrackerListItemView>
        {
            Limit = query.Limit,
            Offset = query.Offset,
            Total = await sql.CountAsync(cancellationToken),
            Items = await sql.Skip(query.Offset).Take(query.Limit).ToListAsync(cancellationToken)
        };
    }
}