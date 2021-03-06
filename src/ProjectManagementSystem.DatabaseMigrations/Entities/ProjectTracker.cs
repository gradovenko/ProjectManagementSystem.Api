using System;

namespace ProjectManagementSystem.DatabaseMigrations.Entities
{
    public sealed class ProjectTracker
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid TrackerId { get; set; }
        public Tracker Tracker { get; set; }
    }
}