using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.User.CreateProjectIssues
{
    public interface IUserRepository
    {
        Task<User> Get(Guid id, CancellationToken cancellationToken);
    }
}