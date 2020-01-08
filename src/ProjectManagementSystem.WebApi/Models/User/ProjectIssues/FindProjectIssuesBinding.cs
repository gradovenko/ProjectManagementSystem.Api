using FluentValidation;

namespace ProjectManagementSystem.WebApi.Models.User.ProjectIssues
{
    public sealed class FindProjectIssuesBinding
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

    public sealed class FindProjectIssuesBindingValidator : AbstractValidator<FindProjectIssuesBinding>
    {
        public FindProjectIssuesBindingValidator()
        {
            RuleFor(b => b.Offset)
                .GreaterThanOrEqualTo(0);
            RuleFor(b => b.Limit)
                .InclusiveBetween(2, 1000);
        }
    }
}