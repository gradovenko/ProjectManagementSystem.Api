using System;

namespace ProjectManagementSystem.Queries.Infrastructure.User.ProjectIssues
{
    internal sealed class Issue
    {
        public Guid Id { get; }
        public long Number { get; }
        public string Title { get; }
        public string Description { get; }
        public DateTime CreateDate { get; }
        public DateTime? UpdateDate { get; }
        public DateTime? StartDate { get; }
        public DateTime? DueDate { get; }
        public Guid ProjectId { get; }
        public Project Project { get; }
        public Guid TrackerId { get; }
        public Tracker Tracker { get; }
        public Guid StatusId { get; }
        public IssueStatus Status { get; }
        public Guid PriorityId { get; }
        public IssuePriority Priority { get; }
        public Guid AuthorId { get; }
        public User Author { get; }
        public Guid? AssigneeId { get; }
        public User? Assignee { get; }
    }
}