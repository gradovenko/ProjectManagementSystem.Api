using System;
using System.Collections.Generic;
using FluentValidation;

namespace ProjectManagementSystem.WebApi.Models.Admin.Projects
{
    public sealed class CreateProjectBindModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public bool IsPrivate { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Guid> Trackers { get; set; }
    }
    
    public sealed class CreateProjectBindModelValidator : AbstractValidator<CreateProjectBindModel>
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
            RuleFor(b => b.Trackers)
                .NotNull();
            RuleForEach(b => b.Trackers)
                .NotEmpty();
        }
    }
}