using System;
using FluentValidation;

namespace ProjectManagementSystem.Api.Models.Admin.Members
{
    public sealed class CreateMemberBinding
    {
        /// <summary>
        /// Member identifier
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Project identifier
        /// </summary>
        public Guid ProjectId { get; set; }
        
        /// <summary>
        /// Role identifier
        /// </summary>
        public Guid RoleId { get; set; }
    }
    
    public sealed class CreateMemberBindingValidator : AbstractValidator<CreateMemberBinding>
    {
        public CreateMemberBindingValidator()
        {
            RuleFor(b => b.ProjectId)
                .NotEmpty();
            RuleFor(b => b.RoleId)
                .NotEmpty();
        }
    }
}