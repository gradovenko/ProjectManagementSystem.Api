using System;
using MediatR;

namespace ProjectManagementSystem.Queries.Admin.Trackers
{
    public class TrackerQuery : IRequest<ShortTrackerView>
    {
        public Guid Id { get; }
        
        public TrackerQuery(Guid id)
        {
            Id = id;
        }
    }
}