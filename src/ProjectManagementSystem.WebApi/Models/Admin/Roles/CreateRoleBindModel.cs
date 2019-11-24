using System;
using System.Collections.Generic;

namespace ProjectManagementSystem.WebApi.Models.Admin.Roles
{
    public sealed class CreateRoleBindModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<AddPermissionBindModel> Permissions { get; set; }
    }
}