using System;

namespace ProjectManagementSystem.Queries.User.IssueTimeEntries
{
    public sealed class TimeEntryView
    {
        /// <summary>
        /// Time entry identifier
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Time entry hours
        /// </summary>
        public decimal Hours { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateDate { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime DueDate { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        
        public string ActivityName { get; set; }
    }
}