using System;
using MediatR;

namespace ProjectManagementSystem.Queries.User.Accounts
{
    public class UserQuery : IRequest<UserView>
    {
        public Guid Id { get; }
        
        public UserQuery(Guid id)
        {
            Id = id;
        }
    }
}