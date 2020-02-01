using System;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.TimeEntryActivities
{
    internal sealed class TimeEntryActivity
    {
        public Guid Id { get; }
        public string Name { get; }
        public bool IsActive { get; }
    }
}