using System;

namespace ProjectManagementSystem.Queries.Admin.TimeEntryActivities
{
    public sealed class TimeEntryActivityListItemView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}