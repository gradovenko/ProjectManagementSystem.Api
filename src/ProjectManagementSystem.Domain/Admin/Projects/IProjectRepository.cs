using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.Admin.Projects
{
    public interface IProjectRepository
    {
        Task<Project> Get(Guid id, CancellationToken cancellationToken);

        Task Save(Project project);
    }
}