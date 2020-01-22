using System;
using MediatR;

namespace ProjectManagementSystem.Queries.Admin.Trackers
{
    public sealed class TrackerQuery : IRequest<TrackerView>
    {
        public Guid Id { get; }
        
        public TrackerQuery(Guid id)
        {
            Id = id;
        }
    }
}