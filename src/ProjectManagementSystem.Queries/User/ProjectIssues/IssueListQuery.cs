using System;

namespace ProjectManagementSystem.Queries.User.ProjectIssues
{
    public sealed class IssueListQuery : PageQuery<IssueListItemView>
    {
        public Guid ProjectId { get; set; }

        public IssueListQuery(Guid projectId, int offset, int limit) : base(offset, limit)
        {
            ProjectId = projectId;
        }
    }
}