using System;

namespace ProjectManagementSystem.Domain.Admin.CreateProjects
{
    public sealed class ProjectTracker
    {
        public Guid ProjectId { get; }
        public Guid TrackerId { get; }

        private ProjectTracker() { }
        
        public ProjectTracker(Guid projectId, Guid trackerId)
        {
            ProjectId = projectId;
            TrackerId = trackerId;
        }
    }
}