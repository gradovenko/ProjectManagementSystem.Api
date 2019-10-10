using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.Admin.CreateRoles
{
    public interface IRoleRepository
    {
        Task<Role> Get(Guid id, CancellationToken cancellationToken);

        Task Save(Role role);
    }
}