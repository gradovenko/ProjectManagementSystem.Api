using Microsoft.AspNetCore.Authorization;

namespace ProjectManagementSystem.WebApi.Authorization
{
    public sealed class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}