using System;
using System.Collections.Generic;
using FluentValidation;

namespace ProjectManagementSystem.WebApi.Models.Admin.Projects
{
    public class CreateProjectBindModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public IEnumerable<AddTrackerBindModel> Trackers { get; set; }
    }
    
    public class CreateProjectBindModelValidator : AbstractValidator<CreateProjectBindModel>
    {
        public CreateProjectBindModelValidator()
        {
            RuleFor(b => b.Id)
                .NotEmpty();
            RuleFor(b => b.Name)
                .NotEmpty();
            RuleFor(b => b.Description)
                .NotNull();
            RuleFor(b => b.IsPrivate)
                .NotNull();
        }
    }
}