using System;

namespace ProjectManagementSystem.Domain.User.TimeEntries
{
    public sealed class TimeEntry
    {
        public Guid Id { get; private set; }
        public decimal Hours { get; private set; }
        public string Description { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime CreateDate { get; private set; }
        public Guid ProjectId { get; private set; }
        public Project Project { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public Guid ActivityId { get; private set; }
        public TimeEntryActivity Activity { get; private set; }
        private Guid _concurrencyStamp = Guid.NewGuid();

        public TimeEntry(Guid id, decimal hours, string description, DateTime dueDate, Guid projectId, Guid userId, Guid activityId)
        {
            Id = id;
            Hours = hours;
            Description = description;
            DueDate = dueDate;
            CreateDate = DateTime.UtcNow;
            ProjectId = projectId;
            UserId = userId;
            ActivityId = activityId;
        }
    }
}