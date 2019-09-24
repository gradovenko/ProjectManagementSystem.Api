using System;
using EventFlow.Queries;

namespace ProjectManagementSystem.Queries.Admin.IssuePriorities
{
    public class IssuePriorityQuery : IQuery<IssuePriorityView>
    {
        public Guid Id { get; }
        
        public IssuePriorityQuery(Guid id)
        {
            Id = id;
        }
    }
}