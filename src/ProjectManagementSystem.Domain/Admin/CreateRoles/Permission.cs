using System;

namespace ProjectManagementSystem.Domain.Admin.CreateRoles
{
    public sealed class Permission
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}