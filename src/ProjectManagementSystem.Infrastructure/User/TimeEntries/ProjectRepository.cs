using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagementSystem.Domain.User.TimeEntries;

namespace ProjectManagementSystem.Infrastructure.User.TimeEntries
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly TimeEntryDbContext _context;

        public ProjectRepository(TimeEntryDbContext context)
        {
            _context = context;
        }
        
        public Task<Project> Get(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}