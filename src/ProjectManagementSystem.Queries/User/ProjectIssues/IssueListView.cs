using System;

namespace ProjectManagementSystem.Queries.User.ProjectIssues
{
    public sealed class IssueListView
    {
        public Guid Id { get; set; }
        public long Index { get; set; }
        public string Title { get; set; }
        public string TrackerName { get; set; }
        public string StatusName { get; set; }
        public string PriorityName { get; set; }
        public string PerformerName { get; set; }
    }
}