using System;

namespace ProjectManagementSystem.WebApi.Models.User.ProjectIssues
{
    public sealed class CreateIssueBindModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid TrackerId { get; set; }
        public Guid StatusId { get; set; }
        public Guid PriorityId { get; set; }
        public Guid PerformerId { get; set; }
    }
}