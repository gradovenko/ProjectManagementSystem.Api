using System;
using MediatR;

namespace ProjectManagementSystem.Queries.User.Accounts
{
    public sealed class UserQuery : IRequest<UserView>
    {
        public Guid Id { get; }
        
        public UserQuery(Guid id)
        {
            Id = id;
        }
    }
}