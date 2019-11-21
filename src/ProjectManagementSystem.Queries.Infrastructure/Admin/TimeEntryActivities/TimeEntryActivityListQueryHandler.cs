using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.TimeEntryActivities;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.TimeEntryActivities
{
    public sealed class TimeEntryActivityListQueryHandler : IRequestHandler<TimeEntryActivityListQuery, Page<TimeEntryActivityListViewModel>>
    {
        private readonly TimeEntryActivityDbContext _context;

        public TimeEntryActivityListQueryHandler(TimeEntryActivityDbContext context)
        {
            _context = context;
        }

        public async Task<Page<TimeEntryActivityListViewModel>> Handle(TimeEntryActivityListQuery query,
            CancellationToken cancellationToken)
        {
            var sql = _context.TimeEntryActivities.AsNoTracking()
                .Select(timeEntryActivity => new TimeEntryActivityListViewModel
                {
                    Id = timeEntryActivity.Id,
                    Name = timeEntryActivity.Name,
                    IsActive = timeEntryActivity.IsActive
                });

            return new Page<TimeEntryActivityListViewModel>
            {
                Limit = query.Limit,
                Offset = query.Offset,
                Total = await sql.CountAsync(cancellationToken),
                Items = await sql.Skip(query.Offset).Take(query.Limit).ToListAsync(cancellationToken)
            };
        }
    }
}