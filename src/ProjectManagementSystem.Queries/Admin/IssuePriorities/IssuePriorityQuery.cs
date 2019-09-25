using System;
using MediatR;

namespace ProjectManagementSystem.Queries.Admin.IssuePriorities
{
    public class IssuePriorityQuery : IRequest<IssuePriorityView>
    {
        public Guid Id { get; }
        
        public IssuePriorityQuery(Guid id)
        {
            Id = id;
        }
    }
}