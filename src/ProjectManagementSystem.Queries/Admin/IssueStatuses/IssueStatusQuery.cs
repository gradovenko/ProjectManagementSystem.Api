using System;
using MediatR;

namespace ProjectManagementSystem.Queries.Admin.IssueStatuses
{
    public class IssueStatusQuery : IRequest<ShortIssueStatusView>
    {
        public Guid Id { get; }
        
        public IssueStatusQuery(Guid id)
        {
            Id = id;
        }
    }
}