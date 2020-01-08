using System;

namespace ProjectManagementSystem.Queries.User.Projects
{
    public sealed class ProjectListView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
    }
}