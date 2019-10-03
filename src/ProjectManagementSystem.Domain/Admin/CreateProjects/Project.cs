using System;
using System.Collections.Generic;

namespace ProjectManagementSystem.Domain.Admin.CreateProjects
{
    public sealed class Project
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public bool IsPublic { get; }
        public ProjectStatus Status { get; }
        public DateTime CreateDate { get; }
        
        private List<ProjectTracker> _projectTrackers = new List<ProjectTracker>();
        public IEnumerable<ProjectTracker> ProjectTrackers => _projectTrackers;
        
        private Guid _concurrencyStamp = Guid.NewGuid();

        public Project(Guid id, string name, string description, bool isPublic)
        {
            Id = id;
            Name = name;
            Description = description;
            IsPublic = isPublic;
            Status = ProjectStatus.Active;
            CreateDate = DateTime.UtcNow;
        }
        
        public void AddProjectTracker(ProjectTracker projectTracker)
        {
            _projectTrackers.Add(projectTracker);
            
            _concurrencyStamp = Guid.NewGuid();
        }
    }
}