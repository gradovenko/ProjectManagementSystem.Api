using System;

namespace ProjectManagementSystem.Domain.Admin.Roles
{
    public sealed class RolePermission
    {
        public Guid RoleId { get; }
        public string PermissionId { get; }

        public RolePermission(Guid roleId, string permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }
    }
}