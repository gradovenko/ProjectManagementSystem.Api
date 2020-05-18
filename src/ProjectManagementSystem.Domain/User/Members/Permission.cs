using System.Collections.Generic;

namespace ProjectManagementSystem.Domain.User.Members
{
    public sealed class Permission
    {
        public string Id { get; }
        private List<RolePermission> _rolePermissions = new List<RolePermission>();
        public IEnumerable<RolePermission> RolePermissions => _rolePermissions;
    }
}