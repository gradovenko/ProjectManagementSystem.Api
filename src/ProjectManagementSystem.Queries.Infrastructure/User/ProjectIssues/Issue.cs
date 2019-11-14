using System;

namespace ProjectManagementSystem.Queries.Infrastructure.User.ProjectIssues
{
    internal sealed class Issue
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public Guid TrackerId { get; private set; }
        public Tracker Tracker { get; private set; }
        public Guid StatusId { get; private set; }
        public IssueStatus Status { get; private set; }
        public Guid PriorityId { get; private set; }
        public IssuePriority Priority { get; private set; }
        public Guid AuthorId { get; private set; }
        public User Author { get; private set; }
        public Guid? PerformerId { get; private set; }
        public User? Performer { get; private set; }
    }
}