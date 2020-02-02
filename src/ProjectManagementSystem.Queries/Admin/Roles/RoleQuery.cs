using System;
using MediatR;

namespace ProjectManagementSystem.Queries.Admin.Roles
{
    public sealed class RoleQuery : IRequest<RoleView>
    {
        public Guid Id { get; }

        public RoleQuery(Guid id)
        {
            Id = id;
        }
    }
}