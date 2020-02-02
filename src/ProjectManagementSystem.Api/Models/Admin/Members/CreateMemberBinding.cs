using System;
using System.Collections.Generic;
using FluentValidation;

namespace ProjectManagementSystem.Api.Models.Admin.Members
{
    public sealed class CreateMemberBinding
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<CreateProjectRoleBinding> ProjectRoles { get; set; }
    }
    
    public sealed class CreateMemberBindingValidator : AbstractValidator<CreateMemberBinding>
    {
        public CreateMemberBindingValidator()
        {
            RuleForEach(b => b.ProjectRoles)
                .NotEmpty();
        }
    }
}