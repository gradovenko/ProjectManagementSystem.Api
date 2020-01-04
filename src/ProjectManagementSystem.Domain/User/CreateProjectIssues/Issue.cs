using System;

namespace ProjectManagementSystem.Domain.User.CreateProjectIssues
{
    public sealed class Issue
    {
        public Guid Id { get; }
        public string Title { get; }
        public string Description { get; }
        public DateTime CreateDate { get; }
        public DateTime? StartDate { get; }
        public DateTime? DueDate { get; }
        public Guid TrackerId { get; }
        public Tracker Tracker { get; private set; }
        public Guid StatusId { get; }
        public IssueStatus Status { get; private set; }
        public Guid PriorityId { get; }
        public IssuePriority Priority { get; private set; }
        public Guid AuthorId { get; }
        public User Author { get; private set; }
        public Guid? AssigneeId { get;}
        public User Assignee { get; private set; }
        private Guid _concurrencyStamp;

        public Issue(Guid id, string title, string description, DateTime? startDate,
            DateTime? dueDate, Guid trackerId, Guid statusId, Guid priorityId, Guid authorId, Guid? assigneeId)
        {
            Id = id;
            Title = title;
            Description = description;
            CreateDate = DateTime.UtcNow;
            StartDate = startDate;
            DueDate = dueDate;
            TrackerId = trackerId;
            StatusId = statusId;
            PriorityId = priorityId;
            AuthorId = authorId;
            AssigneeId = assigneeId;
            _concurrencyStamp = Guid.NewGuid();
        }
    }
}