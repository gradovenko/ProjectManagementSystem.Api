using System;

namespace ProjectManagementSystem.WebApi.Models.User.Issues
{
    public class CreateIssueBindModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid TrackerId { get; set; }
    }
}