using System;
using FluentValidation;

namespace ProjectManagementSystem.WebApi.Models.Admin.IssuePriorities
{
    public class CreateIssuePriorityBinding
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateIssuePriorityValidator : AbstractValidator<CreateIssuePriorityBinding>
    {
        public CreateIssuePriorityValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty();

            RuleFor(r => r.Name)
                .NotEmpty();
        }
    }
}