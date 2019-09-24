using System;
using MediatR;

namespace ProjectManagementSystem.Queries.Admin.Users
{
    public class UserQuery : IRequest<ShortUserView>
    {
        public Guid Id { get; }
        
        public UserQuery(Guid id)
        {
            Id = id;
        }
    }
}