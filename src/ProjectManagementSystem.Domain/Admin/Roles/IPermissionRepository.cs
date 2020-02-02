using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.Admin.Roles
{
    public interface IPermissionRepository
    {
        Task<Permission> Get(Guid id, CancellationToken cancellationToken);
    }
}