using System;
using System.Collections.Generic;

namespace ProjectManagementSystem.Domain.Admin.CreateRoles
{
    public sealed class Role
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        private List<RolePermission> _rolePermissions = new List<RolePermission>();
        public IEnumerable<RolePermission> RolePermissions => _rolePermissions;
        private Guid _concurrencyStamp = Guid.NewGuid();

        public Role(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        
        public void AddRolePermission(RolePermission rolePermission)
        {
            _rolePermissions.Add(rolePermission);
            
            _concurrencyStamp = Guid.NewGuid();
        }
    }
}