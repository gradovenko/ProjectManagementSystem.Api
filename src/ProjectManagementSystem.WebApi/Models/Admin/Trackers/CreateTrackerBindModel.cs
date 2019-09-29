using System;

namespace ProjectManagementSystem.WebApi.Models.Admin.Trackers
{
    public class CreateTrackerBindModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}