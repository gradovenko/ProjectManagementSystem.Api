using System;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.Projects
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public ProjectStatus Status { get; }
        public DateTime CreateDate { get; set; }
    }
}