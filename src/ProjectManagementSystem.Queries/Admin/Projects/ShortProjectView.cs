using System;

namespace ProjectManagementSystem.Queries.Admin.Projects
{
    public class ShortProjectView
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}