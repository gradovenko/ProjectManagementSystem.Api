using System;

namespace ProjectManagementSystem.WebApi.Models.User.TimeEntries
{
    public sealed class CreateTimeEntryBindModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public decimal Hours { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid ActivityId { get; set; }
    }
}