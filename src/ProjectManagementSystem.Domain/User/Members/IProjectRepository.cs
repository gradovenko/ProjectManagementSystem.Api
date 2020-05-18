using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.User.Members
{
    public interface IProjectRepository
    {
        Task<Project> Get(Guid id, CancellationToken cancellationToken);
    }
}