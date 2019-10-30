using System;
using MediatR;

namespace ProjectManagementSystem.Queries.User.ProjectIssues
{
    public sealed class IssueQuery : IRequest<IssueView>
    {
        public Guid Id { get; }
        
        public IssueQuery(Guid id)
        {
            Id = id;
        }
    }
}