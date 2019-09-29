using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.Trackers;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.Trackers
{
    public sealed class TrackersQueryHandler : IRequestHandler<TrackersQuery, Page<FullTrackerView>>
    {
        private readonly TrackerDbContext _context;

        public TrackersQueryHandler(TrackerDbContext context)
        {
            _context = context;
        }
        
        public async Task<Page<FullTrackerView>> Handle(TrackersQuery query, CancellationToken cancellationToken)
        {
            var sql = _context.Trackers.AsNoTracking()
                .Select(project => new FullTrackerView
                {
                    Id = project.Id,
                    Name = project.Name
                });

            return new Page<FullTrackerView>
            {
                Limit = query.Limit,
                Offset = query.Offset,
                Total = await sql.CountAsync(cancellationToken),
                Items = await sql.Skip(query.Offset).Take(query.Limit).ToArrayAsync(cancellationToken)
            };
        }
    }
}