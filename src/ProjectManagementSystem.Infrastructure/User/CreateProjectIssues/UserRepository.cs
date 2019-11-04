using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagementSystem.Domain.User.CreateProjectIssues;

namespace ProjectManagementSystem.Infrastructure.User.CreateProjectIssues
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly ProjectDbContext _context;

        public UserRepository(ProjectDbContext context)
        {
            _context = context;
        }
        
        public Task<Domain.User.CreateProjectIssues.User> Get(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}