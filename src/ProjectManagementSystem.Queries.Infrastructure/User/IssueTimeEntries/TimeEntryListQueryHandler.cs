using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.User.IssueTimeEntries;

namespace ProjectManagementSystem.Queries.Infrastructure.User.IssueTimeEntries;

public sealed class TimeEntryListQueryHandler : IRequestHandler<TimeEntryListQuery, Page<TimeEntryListItemView>>
{
    private readonly TimeEntryDbContext _context;

    public TimeEntryListQueryHandler(TimeEntryDbContext context)
    {
        _context = context;
    }

    public async Task<Page<TimeEntryListItemView>> Handle(TimeEntryListQuery query, CancellationToken cancellationToken)
    {
        var sql = _context.TimeEntries.AsNoTracking()
            .OrderBy(te => te.CreateDate)
            .Where(te => te.IssueId == query.Id)
            .Select(te => new TimeEntryListItemView
            {
                Id = te.Id,
                Hours = te.Hours,
                Description = te.Description,
                CreateDate = te.CreateDate,
                UpdateDate = te.UpdateDate,
                DueDate = te.DueDate,
                UserName = te.User.Name,
                ActivityName = te.Activity.Name
            })
            .AsQueryable();

        return new Page<TimeEntryListItemView>
        {
            Limit = query.Limit,
            Offset = query.Offset,
            Total = await sql.CountAsync(cancellationToken),
            Items = await sql.Skip(query.Offset).Take(query.Limit).ToListAsync(cancellationToken)
        };
    }
}