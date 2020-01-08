using System;

namespace ProjectManagementSystem.Domain.Admin.Trackers
{
    public sealed class Tracker
    {
        public Guid Id { get; }
        public string Name { get; }
        private Guid _concurrencyStamp;

        public Tracker(Guid id, string name)
        {
            Id = id;
            Name = name;
            _concurrencyStamp = Guid.NewGuid();
        }
    }
}