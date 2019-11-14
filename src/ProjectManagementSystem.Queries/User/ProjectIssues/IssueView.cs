using System;

namespace ProjectManagementSystem.Queries.User.ProjectIssues
{
    public sealed class IssueView
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StatusName { get; set; }
        public string PriorityName { get; set; }
        public AuthorView Author { get;  set; }
        public PerformerView Performer { get; set; }
    }
}