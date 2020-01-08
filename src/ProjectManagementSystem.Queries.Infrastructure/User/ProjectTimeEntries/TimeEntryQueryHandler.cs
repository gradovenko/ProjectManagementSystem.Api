using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.User.ProjectTimeEntries;

namespace ProjectManagementSystem.Queries.Infrastructure.User.ProjectTimeEntries
{
    public sealed class TimeEntryQueryHandler : IRequestHandler<TimeEntryQuery, TimeEntryView>
    {
        private readonly TimeEntryDbContext _context;

        public TimeEntryQueryHandler(TimeEntryDbContext context)
        {
            _context = context;
        }

        public async Task<TimeEntryView> Handle(TimeEntryQuery query, CancellationToken cancellationToken)
        {
            return await _context.TimeEntries
                .Include(i => i.Project)
                .Include(i => i.Issue)
                .Include(i => i.User)
                .Include(i => i.Activity)
                .AsNoTracking()
                .Where(te => te.ProjectId == query.ProjectId && te.Id == query.TimeEntryId)
                .Select(te => new TimeEntryView
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
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}