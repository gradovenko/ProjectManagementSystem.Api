using System;

namespace ProjectManagementSystem.Queries.User.Projects
{
    public class ProjectsView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
    }
}