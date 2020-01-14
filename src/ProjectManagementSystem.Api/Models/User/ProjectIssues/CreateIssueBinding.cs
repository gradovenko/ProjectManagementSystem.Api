using System;
using FluentValidation;

namespace ProjectManagementSystem.Api.Models.User.ProjectIssues
{
    public sealed class CreateIssueBinding
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid TrackerId { get; set; }
        public Guid StatusId { get; set; }
        public Guid PriorityId { get; set; }
        public Guid? AssigneeId { get; set; }
    }

    public sealed class CreateIssueBindingValidator : AbstractValidator<CreateIssueBinding>
    {
        public CreateIssueBindingValidator()
        {
            RuleFor(b => b.Id)
                .NotEmpty();
            RuleFor(b => b.Title)
                .NotEmpty();
            RuleFor(b => b.Description)
                .NotNull();
            RuleFor(b => b.StartDate)
                .NotEmpty()
                .When(b => b.StartDate != null);
            RuleFor(b => b.DueDate)
                .NotEmpty()
                .When(b => b.DueDate != null);
            RuleFor(b => b.TrackerId)
                .NotEmpty();
            RuleFor(b => b.StatusId)
                .NotEmpty();
            RuleFor(b => b.PriorityId)
                .NotEmpty();
            RuleFor(b => b.AssigneeId)
                .NotEmpty()
                .When(b => b.AssigneeId != null);
        }
    }
}