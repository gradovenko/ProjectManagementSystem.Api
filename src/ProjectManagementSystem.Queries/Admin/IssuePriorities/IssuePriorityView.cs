using System;

namespace ProjectManagementSystem.Queries.Admin.IssuePriorities
{
    public sealed class IssuePriorityView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}