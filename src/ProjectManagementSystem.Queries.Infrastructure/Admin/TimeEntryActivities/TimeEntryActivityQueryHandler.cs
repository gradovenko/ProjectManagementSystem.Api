using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.TimeEntryActivities;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.TimeEntryActivities;

public sealed class TimeEntryActivityQueryHandler : IRequestHandler<TimeEntryActivityQuery, TimeEntryActivityView>
{
    private readonly TimeEntryActivityDbContext _context;

    public TimeEntryActivityQueryHandler(TimeEntryActivityDbContext context)
    {
        _context = context;
    }

    public async Task<TimeEntryActivityView> Handle(TimeEntryActivityQuery query, CancellationToken cancellationToken)
    {
        return await _context.TimeEntryActivities.AsNoTracking()
            .Where(timeEntryActivity => timeEntryActivity.Id == query.Id)
            .Select(timeEntryActivity => new TimeEntryActivityView
            {
                Id = timeEntryActivity.Id,
                Name = timeEntryActivity.Name,
                IsActive = timeEntryActivity.IsActive
            })
            .SingleOrDefaultAsync(cancellationToken);
    }
}