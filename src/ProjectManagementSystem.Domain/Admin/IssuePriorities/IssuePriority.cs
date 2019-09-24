using System;

namespace ProjectManagementSystem.Domain.Admin.IssuePriorities
{
    public class IssuePriority
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }

        public IssuePriority(Guid id, string name, bool isActive)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
        }

        protected IssuePriority() { }
    }
}