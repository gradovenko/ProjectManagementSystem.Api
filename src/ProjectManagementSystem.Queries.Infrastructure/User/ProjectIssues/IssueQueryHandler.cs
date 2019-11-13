using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProjectManagementSystem.Queries.User.ProjectIssues;

namespace ProjectManagementSystem.Queries.Infrastructure.User.ProjectIssues
{
    public sealed class IssueQueryHandler : IRequestHandler<IssueQuery, IssueView>
    {
        private readonly ProjectDbContext _context;

        public IssueQueryHandler(ProjectDbContext context)
        {
            _context = context;
        }

        public Task<IssueView> Handle(IssueQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}