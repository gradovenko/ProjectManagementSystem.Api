using System;

namespace ProjectManagementSystem.DatabaseMigrations.Entities
{
    public sealed class Issue
    {
        public Guid Id { get; set; }
        public long Index { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime? StartDate { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid TrackerId { get; set; }
        public Tracker Tracker { get; set; }
        public Guid IssueStatusId { get; set; }
        public IssueStatus IssueStatus { get; set; }
        public Guid IssuePriorityId { get; set; }
        public IssuePriority IssuePriority { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ConcurrencyStamp { get; set; }
    }
}