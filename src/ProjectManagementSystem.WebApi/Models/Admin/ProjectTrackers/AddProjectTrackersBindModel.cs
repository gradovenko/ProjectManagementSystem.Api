using System;
using System.Collections.Generic;

namespace ProjectManagementSystem.WebApi.Models.Admin.ProjectTrackers
{
    public class AddProjectTrackersBindModel
    {
        public IEnumerable<AddTrackerBindModel> Trackers { get; set; }
    }
}