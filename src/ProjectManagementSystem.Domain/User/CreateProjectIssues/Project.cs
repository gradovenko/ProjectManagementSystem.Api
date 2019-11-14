using System;
using System.Collections.Generic;

namespace ProjectManagementSystem.Domain.User.CreateProjectIssues
{
    public sealed class Project
    {
        public Guid Id { get; private set; }

        private List<Issue> _issues = new List<Issue>();
        public IEnumerable<Issue> Issues => _issues;

        public void AddIssue(Issue issue)
        {
            _issues.Add(issue);
        }
    }
}