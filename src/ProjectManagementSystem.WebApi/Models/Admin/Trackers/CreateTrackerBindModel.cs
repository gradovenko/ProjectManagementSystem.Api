using System;
using FluentValidation;

namespace ProjectManagementSystem.WebApi.Models.Admin.Trackers
{
    public class CreateTrackerBindModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    
    public class CreateTrackerBindModelValidator : AbstractValidator<CreateTrackerBindModel>
    {
        public CreateTrackerBindModelValidator()
        {
            RuleFor(b => b.Id)
                .NotEmpty();
            RuleFor(b => b.Name)
                .NotEmpty();
        }
    }
}