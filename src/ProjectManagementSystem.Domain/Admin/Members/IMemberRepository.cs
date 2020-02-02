using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.Admin.Members
{
    public interface IMemberRepository
    {
        Task<Member> Get(Guid id, CancellationToken cancellationToken);
    }
}