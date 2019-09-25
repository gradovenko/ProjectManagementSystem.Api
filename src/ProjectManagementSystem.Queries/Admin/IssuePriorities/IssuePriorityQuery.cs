using System;
using MediatR;

namespace ProjectManagementSystem.Queries.Admin.IssuePriorities
{
    public class IssuePriorityQuery : IRequest<ShortIssuePriorityView>
    {
        public Guid Id { get; }
        
        public IssuePriorityQuery(Guid id)
        {
            Id = id;
        }
    }
}