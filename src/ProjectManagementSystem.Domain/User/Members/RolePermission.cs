using System;

namespace ProjectManagementSystem.Domain.User.Members
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