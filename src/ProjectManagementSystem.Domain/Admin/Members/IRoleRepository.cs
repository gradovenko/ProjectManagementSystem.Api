using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.Admin.Members
{
    public interface IRoleRepository
    {
        Task<Role> Get(Guid id, CancellationToken cancellationToken);
    }
}