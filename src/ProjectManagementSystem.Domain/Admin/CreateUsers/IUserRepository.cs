using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.Admin.CreateUsers
{
    public interface IUserRepository
    {
        Task<User> Get(Guid id, CancellationToken cancellationToken);
        Task<User> FindByName(string name, CancellationToken cancellationToken);
        Task<User> FindByEmail(string email, CancellationToken cancellationToken);
        Task Save(User user);
    }
}