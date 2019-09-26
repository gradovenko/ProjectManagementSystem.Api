using System;
using ProjectManagementSystem.DatabaseMigrations.Entities;

namespace ProjectManagementSystem.Domain.Admin.Projects
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public ProjectStatus Status { get; set; }
        public DateTime CreateDate { get; set; }

        public Project(Guid id, string name, string description, bool isPublic)
        {
            Id = id;
            Name = name;
            Description = description;
            IsPublic = isPublic;
            Status = ProjectStatus.Active;
            CreateDate = DateTime.UtcNow;
        }
    }
}