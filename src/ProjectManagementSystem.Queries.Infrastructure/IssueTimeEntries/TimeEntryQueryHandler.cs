using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.User.IssueTimeEntries;

namespace ProjectManagementSystem.Queries.Infrastructure.IssueTimeEntries;

public sealed class TimeEntryQueryHandler : IRequestHandler<TimeEntryListQuery, IEnumerable<TimeEntryListItemViewModel>> 
{
    private readonly TimeEntryQueryDbContext _context;

    public TimeEntryQueryHandler(TimeEntryQueryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<IEnumerable<TimeEntryListItemViewModel>> Handle(TimeEntryListQuery request, CancellationToken cancellationToken)
    {
        return await _context.TimeEntries.AsNoTracking()
            .Include(o => o.Author)
            .Where(o => o.IssueId == request.IssueId)
            .Select(o => new TimeEntryListItemViewModel
            {
                Id = o.TimeEntryId,
                Hours = o.Hours,
                Description = o.Description,
                DueDate = o.DueDate,
                CreateDate = o.CreateDate,
                IsDeleted = o.IsDeleted,
                Author = new AuthorViewModel
                {
                    Id = o.Author.UserId,
                    Name = o.Author.Name
                }
            }).ToListAsync(cancellationToken);
    }
}