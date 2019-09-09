using System;
using EventFlow.Queries;

namespace ProjectManagementSystem.Queries.User.Accounts
{
    public class UserQuery : IQuery<UserView>
    {
        public Guid Id { get; }
        
        public UserQuery(Guid id)
        {
            Id = id;
        }
    }
}