using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.Admin.Roles
{
    public interface IPermissionRepository
    {
        Task<Permission> Get(string id, CancellationToken cancellationToken);
    }
}