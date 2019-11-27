using System;
using FluentValidation;

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

    public sealed class CreateTimeEntryBindModelValidator : AbstractValidator<CreateTimeEntryBindModel>
    {
        public CreateTimeEntryBindModelValidator()
        {
            RuleFor(b => b.Id)
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