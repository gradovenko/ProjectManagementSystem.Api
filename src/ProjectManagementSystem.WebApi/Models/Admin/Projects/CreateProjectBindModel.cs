using System;

namespace ProjectManagementSystem.WebApi.Models.Admin.Projects
{
    public class CreateProjectBindModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
    }
}