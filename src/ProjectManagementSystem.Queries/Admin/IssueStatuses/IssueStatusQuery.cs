using System;
using MediatR;

namespace ProjectManagementSystem.Queries.Admin.IssueStatuses
{
    public sealed class IssueStatusQuery : IRequest<IssueStatusView>
    {
        public Guid Id { get; }
        
        public IssueStatusQuery(Guid id)
        {
            Id = id;
        }
    }
}