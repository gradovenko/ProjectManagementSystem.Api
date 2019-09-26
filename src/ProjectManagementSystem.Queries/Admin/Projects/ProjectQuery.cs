using System;
using MediatR;

namespace ProjectManagementSystem.Queries.Admin.Projects
{
    public class ProjectQuery : IRequest<ShortProjectView>
    {
        public Guid Id { get; }
        
        public ProjectQuery(Guid id)
        {
            Id = id;
        }
    }
}