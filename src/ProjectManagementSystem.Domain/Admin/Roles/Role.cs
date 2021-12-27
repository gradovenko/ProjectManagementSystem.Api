using System;
using System.Collections.Generic;

namespace ProjectManagementSystem.Domain.Admin.Roles
{
    public sealed class Role
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        private List<RolePermission> _rolePermissions = new List<RolePermission>();
        public IEnumerable<RolePermission> RolePermissions => _rolePermissions;
        private Guid _concurrencyStamp;

        public Role(Guid id, string name)
        {
            Id = id;
            Name = name;
            _concurrencyStamp = Guid.NewGuid();
        }
        
        public void AddRolePermission(RolePermission rolePermission)
        {
            _rolePermissions.Add(rolePermission);
            _concurrencyStamp = Guid.NewGuid();
        }
    }
}