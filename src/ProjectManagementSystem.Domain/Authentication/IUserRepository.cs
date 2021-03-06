using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.Authentication
{
    public interface IUserRepository
    {
        Task<User> Get(Guid id, CancellationToken cancellationToken);
        Task<User> GetByName(string name, CancellationToken cancellationToken);
        Task<User> GetByEmail(string email, CancellationToken cancellationToken);
    }
}