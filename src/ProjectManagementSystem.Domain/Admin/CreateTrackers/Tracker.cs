using System;

namespace ProjectManagementSystem.Domain.Admin.CreateTrackers
{
    public sealed class Tracker
    {
        public Guid Id { get; }
        public string Name { get; }
        private Guid _concurrencyStamp = Guid.NewGuid();

        public Tracker(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}