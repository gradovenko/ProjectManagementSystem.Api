using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProjectManagementSystem.Queries.User.ProjectIssues;

namespace ProjectManagementSystem.Queries.Infrastructure.User.ProjectIssues
{
    public sealed class IssueListQueryHandler : IRequestHandler<IssueListQuery, Page<IssueListView>>
    {
        private readonly ProjectDbContext _context;

        public IssueListQueryHandler(ProjectDbContext context)
        {
            _context = context;
        }
        
        public Task<Page<IssueListView>> Handle(IssueListQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}