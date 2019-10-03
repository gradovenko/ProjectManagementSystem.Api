using System;
using System.Collections.Generic;

namespace ProjectManagementSystem.Domain.Admin.CreateProjectTrackers
{
    public sealed class Project
    {
        public Guid Id { get; }
        
        private List<ProjectTracker> _projectTrackers = new List<ProjectTracker>();
        public IEnumerable<ProjectTracker> ProjectTrackers => _projectTrackers;
        
        private Guid _concurrencyStamp = Guid.NewGuid();

        public void AddProjectTracker(ProjectTracker projectTracker)
        {
            _projectTrackers.Add(projectTracker);
            
            _concurrencyStamp = Guid.NewGuid();
        }
    }
}