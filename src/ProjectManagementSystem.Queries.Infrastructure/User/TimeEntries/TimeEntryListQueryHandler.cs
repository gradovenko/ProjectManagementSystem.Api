using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.User.TimeEntries;

namespace ProjectManagementSystem.Queries.Infrastructure.User.TimeEntries;

public sealed class TimeEntryListQueryHandler : IRequestHandler<TimeEntryListQuery, PageViewModel<TimeEntryListItemView>>
{
    private readonly TimeEntryDbContext _context;

    public TimeEntryListQueryHandler(TimeEntryDbContext context)
    {
        _context = context;
    }

    public async Task<PageViewModel<TimeEntryListItemView>> Handle(TimeEntryListQuery query, CancellationToken cancellationToken)
    {
        var sql = _context.TimeEntries.AsNoTracking()
            .OrderBy(te => te.CreateDate)
            .Select(te => new TimeEntryListItemView
            {
                Id = te.Id,
                Hours = te.Hours,
                Description = te.Description,
                CreateDate = te.CreateDate,
                UpdateDate = te.UpdateDate,
                DueDate = te.DueDate,
                IssueNumber = te.Issue.Number,
                UserName = te.User.Name,
                ActivityName = te.Activity.Name
            })
            .AsQueryable();

        return new PageViewModel<TimeEntryListItemView>
        {
            Limit = query.Limit,
            Offset = query.Offset,
            Total = await sql.CountAsync(cancellationToken),
            Items = await sql.Skip(query.Offset).Take(query.Limit).ToListAsync(cancellationToken)
        };
    }
}