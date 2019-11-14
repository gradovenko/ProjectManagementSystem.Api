using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.User.ProjectIssues;

namespace ProjectManagementSystem.Queries.Infrastructure.User.ProjectIssues
{
    public sealed class IssueQueryHandler : IRequestHandler<IssueQuery, IssueView>
    {
        private readonly IssueDbContext _context;

        public IssueQueryHandler(IssueDbContext context)
        {
            _context = context;
        }

        public async Task<IssueView> Handle(IssueQuery query, CancellationToken cancellationToken)
        {
            var issue = await _context.Issues
                .Include(i => i.Project)
                .Include(i => i.Tracker)
                .Include(i => i.Status)
                .Include(i => i.Priority)
                .Include(i => i.Author)
                .Include(i => i.Performer)
                .AsNoTracking()
                .Where(i => i.ProjectId == query.ProjectId && i.Id == query.IssueId)
                .SingleOrDefaultAsync(cancellationToken);

            return new IssueView
            {
                Id = issue.Id,
                Index = issue.Index,
                Title = issue.Title,
                Description = issue.Description,
                CreateDate = issue.CreateDate,
                UpdateDate = issue.UpdateDate,
                StartDate = issue.StartDate,
                EndDate = issue.EndDate,
                TrackerName = issue.Tracker.Name,
                StatusName = issue.Status.Name,
                PriorityName = issue.Priority.Name,
                AuthorName = issue.Author.Name,
                PerformerName = issue.Performer.Name
            };
        }
    }
}