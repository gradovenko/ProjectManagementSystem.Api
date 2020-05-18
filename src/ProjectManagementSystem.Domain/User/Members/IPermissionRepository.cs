using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.User.Members
{
    public interface IPermissionRepository
    {
        Task<Permission> Get(string id, CancellationToken cancellationToken);
    }
}