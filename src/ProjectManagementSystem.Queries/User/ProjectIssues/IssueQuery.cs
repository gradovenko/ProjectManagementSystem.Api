using System;
using MediatR;

namespace ProjectManagementSystem.Queries.User.ProjectIssues
{
    public sealed class IssueQuery : IRequest<IssueView>
    {
        public Guid ProjectId { get; }

        public Guid IssueId { get; }

        public IssueQuery(Guid projectId, Guid issueId)
        {
            ProjectId = projectId;
            IssueId = issueId;
        }
    }
}