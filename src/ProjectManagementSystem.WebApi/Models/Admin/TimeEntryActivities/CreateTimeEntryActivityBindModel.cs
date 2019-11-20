using System;
using FluentValidation;

namespace ProjectManagementSystem.WebApi.Models.Admin.TimeEntryActivities
{
    public sealed class CreateTimeEntryActivityBindModel
    {
        /// <summary>
        /// Time entry identifier
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Time entry name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Time entry status
        /// </summary>
        public bool IsActive { get; set; }
    }
    
    public class CreateTimeEntryActivityBindModelValidator : AbstractValidator<CreateTimeEntryActivityBindModel>
    {
        public CreateTimeEntryActivityBindModelValidator()
        {
            RuleFor(m => m.Id)
                .NotEmpty();
            RuleFor(m => m.Name)
                .NotEmpty();
            RuleFor(m => m.IsActive)
                .NotNull();
        }
    }
}