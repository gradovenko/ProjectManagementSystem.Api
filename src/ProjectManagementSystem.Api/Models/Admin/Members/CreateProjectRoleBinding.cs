using System;
using FluentValidation;

namespace ProjectManagementSystem.Api.Models.Admin.Members
{
    public sealed class CreateProjectRoleBinding
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid ProjectId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Guid RoleId { get; set; }
    }
    
    public sealed class CreateProjectRoleBindingValidator : AbstractValidator<CreateProjectRoleBinding>
    {
        public CreateProjectRoleBindingValidator()
        {
            RuleFor(b => b.ProjectId)
                .NotEmpty();
            RuleFor(b => b.RoleId)
                .NotEmpty();
        }
    }
}