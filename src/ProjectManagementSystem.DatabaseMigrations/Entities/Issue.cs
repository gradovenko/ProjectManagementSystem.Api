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
        public DateTime? UpdateDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid TrackerId { get; set; }
        public Tracker Tracker { get; set; }
        public Guid StatusId { get; set; }
        public IssueStatus Status { get; set; }
        public Guid PriorityId { get; set; }
        public IssuePriority Priority { get; set; }
        public Guid AuthorId { get; set; }
        public User Author { get; set; }
        public Guid? AssigneeId { get; set; }
        public User Assignee { get; set; }
        public Guid ConcurrencyStamp { get; set; }
    }
}