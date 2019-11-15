using System;

namespace ProjectManagementSystem.Queries.User.ProjectIssues
{
    public sealed class IssueListQuery : PageQuery<IssueListView>
    {
        public Guid ProjectId { get; set; }

        public IssueListQuery(Guid projectId, int limit, int offset) : base(limit, offset)
        {
            ProjectId = projectId;
        }
    }
}