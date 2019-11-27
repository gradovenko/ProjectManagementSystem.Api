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
            return await _context.Issues
                .Include(i => i.Project)
                .Include(i => i.Tracker)
                .Include(i => i.Status)
                .Include(i => i.Priority)
                .Include(i => i.Author)
                .Include(i => i.Assignee)
                .AsNoTracking()
                .Where(i => i.ProjectId == query.ProjectId && i.Id == query.IssueId)
                .Select(i => new IssueView
                {
                    Id = i.Id,
                    Index = i.Index,
                    Title = i.Title,
                    Description = i.Description,
                    CreateDate = i.CreateDate,
                    UpdateDate = i.UpdateDate,
                    StartDate = i.StartDate,
                    DueDate = i.DueDate,
                    TrackerName = i.Tracker.Name,
                    StatusName = i.Status.Name,
                    PriorityName = i.Priority.Name,
                    AuthorName = i.Author.Name,
                    AssigneeName = i.Assignee.Name
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}