using System;

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
    }
}