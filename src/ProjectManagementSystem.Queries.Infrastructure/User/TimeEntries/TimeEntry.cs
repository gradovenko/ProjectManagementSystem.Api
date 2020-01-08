using System;

namespace ProjectManagementSystem.Queries.Infrastructure.User.TimeEntries
{
    internal sealed class TimeEntry
    {
        public Guid Id { get; private set; }
        public decimal Hours { get; private set; }
        public string Description { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public Guid ProjectId { get; private set; }
        public Project Project { get; private set; }
        public Guid IssueId { get; private set; }
        public Issue Issue { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public Guid ActivityId { get; private set; }
        public TimeEntryActivity Activity { get; private set; }
    }
}