using System;

namespace ProjectManagementSystem.Queries.Admin.IssueStatuses
{
    public sealed class IssueStatusListItemView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}