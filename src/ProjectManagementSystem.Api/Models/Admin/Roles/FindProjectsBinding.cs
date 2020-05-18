using FluentValidation;
using ProjectManagementSystem.Api.Models.Admin.Projects;

namespace ProjectManagementSystem.Api.Models.Admin.Roles
{
    public sealed class FindRolesBinding
    {
        /// <summary>
        /// Offset
        /// </summary>
        public int Offset { get; set; } = 0;
        
        /// <summary>
        /// Limit
        /// </summary>
        public int Limit { get; set; } = 10;
    }

    public sealed class FindRolesBindingValidator : AbstractValidator<FindProjectsBinding>
    {
        public FindRolesBindingValidator()
        {
            RuleFor(b => b.Offset)
                .GreaterThanOrEqualTo(0);
            RuleFor(b => b.Limit)
                .InclusiveBetween(2, 1000);
        }
    }
}