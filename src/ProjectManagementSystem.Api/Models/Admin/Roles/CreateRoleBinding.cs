using System;
using System.Collections.Generic;
using FluentValidation;

namespace ProjectManagementSystem.Api.Models.Admin.Roles
{
    public sealed class CreateRoleBinding
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
        public IEnumerable<Guid> Permissions { get; set; }
    }
    
    public sealed class CreateRoleBindingValidator : AbstractValidator<CreateRoleBinding>
    {
        public CreateRoleBindingValidator()
        {
            RuleFor(b => b.Id)
                .NotEmpty();
            RuleFor(b => b.Name)
                .NotEmpty();
            RuleForEach(b => b.Permissions)
                .NotEmpty();
        }
    }
}