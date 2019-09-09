using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.User.Accounts
{
    public interface IUserRepository
    {
        Task<User> Get(Guid id, CancellationToken cancellationToken);
        Task<User> FindByUserName(string userName, CancellationToken cancellationToken);
        Task<User> FindByEmail(string email, CancellationToken cancellationToken);
        Task Save(User user);
    }
}