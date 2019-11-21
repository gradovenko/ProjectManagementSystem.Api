using System;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.TimeEntryActivities
{
    internal sealed class TimeEntryActivity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }
    }
}