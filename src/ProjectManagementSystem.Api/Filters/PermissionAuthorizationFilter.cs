using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Domain.User.Members;

namespace ProjectManagementSystem.Api.Filters
{
    public sealed class PermissionAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly IMemberRepository _userRepository;

        public PermissionAuthorizationFilter(IMemberRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = _userRepository.Get(context.HttpContext.User.GetId(), CancellationToken.None);
            
            
        }
    }
}