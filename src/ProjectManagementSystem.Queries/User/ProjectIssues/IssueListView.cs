using System;

namespace ProjectManagementSystem.Queries.User.ProjectIssues
{
    public sealed class IssueListView
    {
        /// <summary>
        /// Issue identifier
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Issue sequence number
        /// </summary>
        public long Index { get; set; }
        
        /// <summary>
        /// Issue title
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Issue tracker name
        /// </summary>
        public string TrackerName { get; set; }
        
        /// <summary>
        /// Issue status name
        /// </summary>
        public string StatusName { get; set; }
        
        /// <summary>
        /// Issue priority name
        /// </summary>
        public string PriorityName { get; set; }
        
        /// <summary>
        /// Issue assignee name
        /// </summary>
        public string AssigneeName { get; set; }
        
        /// <summary>
        /// Issue update date
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}