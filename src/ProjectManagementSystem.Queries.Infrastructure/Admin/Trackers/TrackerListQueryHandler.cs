using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.Trackers;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.Trackers
{
    public sealed class TrackerListQueryHandler : IRequestHandler<TrackerListQuery, Page<TrackerListItemView>>
    {
        private readonly TrackerDbContext _context;

        public TrackerListQueryHandler(TrackerDbContext context)
        {
            _context = context;
        }
        
        public async Task<Page<TrackerListItemView>> Handle(TrackerListQuery query, CancellationToken cancellationToken)
        {
            var sql = _context.Trackers.AsNoTracking()
                .Select(project => new TrackerListItemView
                {
                    Id = project.Id,
                    Name = project.Name
                });

            return new Page<TrackerListItemView>
            {
                Limit = query.Limit,
                Offset = query.Offset,
                Total = await sql.CountAsync(cancellationToken),
                Items = await sql.Skip(query.Offset).Take(query.Limit).ToListAsync(cancellationToken)
            };
        }
    }
}