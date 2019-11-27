using System;

namespace ProjectManagementSystem.WebApi.Models.User.Issues
{
    public sealed class CreateIssueBindModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid TrackerId { get; set; }
        public Guid StatusId { get; set; }
        public Guid PriorityId { get; set; }
        public Guid AssigneeId { get; set; }
    }
}