using System;

namespace ProjectManagementSystem.Domain.Admin.TimeEntryActivities
{
    public sealed class TimeEntryActivity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        private Guid _concurrencyStamp = Guid.NewGuid();

        public TimeEntryActivity(Guid id, string name, bool isActive)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
        }
    }
}