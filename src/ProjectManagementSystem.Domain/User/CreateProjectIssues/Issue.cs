using System;

namespace ProjectManagementSystem.Domain.User.CreateProjectIssues
{
    public sealed class Issue
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Guid TrackerId { get; private set; }

        public Issue(Guid id, string title, string description, Guid trackerId)
        {
            Id = id;
            Title = title;
            Description = description;
            TrackerId = trackerId;
        }
    }
}