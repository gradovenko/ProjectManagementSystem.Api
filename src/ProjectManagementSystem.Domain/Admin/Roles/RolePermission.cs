using System;

namespace ProjectManagementSystem.Domain.Admin.Roles
{
    public sealed class RolePermission
    {
        public Guid RoleId { get; }
        public Guid PermissionId { get; }

        public RolePermission(Guid roleId, Guid permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }
    }
}