using System;
using MediatR;

namespace ProjectManagementSystem.Queries.Admin.Projects
{
    public sealed class ProjectQuery : IRequest<ProjectView>
    {
        public Guid Id { get; }
        
        public ProjectQuery(Guid id)
        {
            Id = id;
        }
    }
}