using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ProjectManagementSystem.WebApi.Authorization
{
    public class PermissionAuthorizationHandler :  AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            //var a = context.HttpContext.Request.RouteValues.;

        }
    }
}