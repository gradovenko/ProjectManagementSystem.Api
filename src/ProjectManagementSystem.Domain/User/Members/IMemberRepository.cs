using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.User.Members
{
    public interface IMemberRepository
    {
        Task<Member> Get(Guid userId, Guid projectId, Guid roleId, CancellationToken cancellationToken);
    }
}