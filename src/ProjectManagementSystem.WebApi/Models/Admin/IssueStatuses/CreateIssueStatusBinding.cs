using System;
using FluentValidation;

namespace ProjectManagementSystem.WebApi.Models.Admin.IssueStatuses
{
    public class CreateIssueStatusBinding
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateIssueStatusBindingValidator : AbstractValidator<CreateIssueStatusBinding>
    {
        public CreateIssueStatusBindingValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty();

            RuleFor(r => r.Name)
                .NotEmpty();
        }
    }
}