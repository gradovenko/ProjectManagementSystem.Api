using System;
using FluentValidation;

namespace ProjectManagementSystem.WebApi.Models.User.ProjectTimeEntries
{
    public sealed class CreateTimeEntryBinding
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Guid IssueId { get; set; }
        
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
    
    public sealed class CreateTimeEntryBindingValidator : AbstractValidator<CreateTimeEntryBinding>
    {
        public CreateTimeEntryBindingValidator()
        {
            RuleFor(b => b.Id)
                .NotEmpty();
            RuleFor(b => b.IssueId)
                .NotEmpty();
            RuleFor(b => b.Hours)
                .NotEmpty();
            RuleFor(b => b.Description)
                .NotNull();
            RuleFor(b => b.DueDate)
                .NotEmpty();
            RuleFor(b => b.ActivityId)
                .NotEmpty();
        }
    }
}