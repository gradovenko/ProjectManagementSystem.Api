using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.User.TimeEntries;

namespace ProjectManagementSystem.Queries.Infrastructure.User.TimeEntries
{
    public sealed class TimeEntryQueryHandler : IRequestHandler<TimeEntryQuery, TimeEntryViewModel>
    {
        private readonly TimeEntryDbContext _context;

        public TimeEntryQueryHandler(TimeEntryDbContext context)
        {
            _context = context;
        }

        public async Task<TimeEntryViewModel> Handle(TimeEntryQuery query, CancellationToken cancellationToken)
        {
            return await _context.TimeEntries
                .Include(i => i.Project)
                .Include(i => i.Issue)
                .Include(i => i.User)
                .Include(i => i.Activity)
                .AsNoTracking()
                .Where(te => te.ProjectId == query.ProjectId && te.IssueId == query.IssueId && te.Id == query.TimeEntryId)
                .Select(te => new TimeEntryViewModel
                {
                    Id = te.Id,
                    Hours = te.Hours,
                    Description = te.Description,
                    CreateDate = te.CreateDate,
                    UpdateDate = te.UpdateDate,
                    DueDate = te.DueDate,
                    ProjectName = te.Project.Name,
                    IssueNumber = te.Issue.Index,
                    UserName = te.User.Name,
                    ActivityName = te.Activity.Name
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}