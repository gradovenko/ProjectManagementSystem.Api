using System;
using System.Collections.Generic;

namespace ProjectManagementSystem.Domain.User.TimeEntries
{
    public sealed class Issue
    {
        public Guid Id { get; private set; }
        private List<TimeEntry> _timeEntries = new List<TimeEntry>();
        public IEnumerable<TimeEntry> TimeEntries => _timeEntries;
        private Guid _concurrencyStamp = Guid.NewGuid();

        public void AddTimeEntry(TimeEntry timeEntry)
        {
            _timeEntries.Add(timeEntry);
        }
    }
}