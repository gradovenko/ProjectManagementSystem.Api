using System;
using EventFlow.Queries;

namespace ProjectManagementSystem.Queries.Admin.Users
{
    public class UserQuery : IQuery<ShortUserView>
    {
        public Guid Id { get; }
        
        public UserQuery(Guid id)
        {
            Id = id;
        }
    }
}