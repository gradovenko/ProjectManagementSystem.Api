using System;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.Authentication
{
    public interface IRefreshTokenStore
    {
        Task<Guid> Create(Guid userId);
        Task<RefreshToken> Reissue(Guid refreshToken);
    }
}