using System;
using System.Collections.Generic;
using FluentValidation;

namespace ProjectManagementSystem.Api.Models.Admin.Members
{
    public sealed class CreateMembersBinding
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<CreateMemberBinding> Members { get; set; }
    }
    
    public sealed class CreateMembersBindingValidator : AbstractValidator<CreateMembersBinding>
    {
        public CreateMembersBindingValidator()
        {
            RuleForEach(b => b.Members)
                .NotEmpty();
        }
    }
}