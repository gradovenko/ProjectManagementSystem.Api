using System;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.Authentication
{
    public interface IRefreshTokenStore
    {
        Task<string> Create(Guid userId);
        Task<RefreshToken> Reissue(string refreshToken);
    }
}