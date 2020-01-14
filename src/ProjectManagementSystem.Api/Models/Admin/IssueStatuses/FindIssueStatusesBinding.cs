using FluentValidation;

namespace ProjectManagementSystem.Api.Models.Admin.IssueStatuses
{
    public sealed class FindIssueStatusesBinding
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

    public sealed class FindIssueStatusesBindingValidator : AbstractValidator<FindIssueStatusesBinding>
    {
        public FindIssueStatusesBindingValidator()
        {
            RuleFor(b => b.Offset)
                .GreaterThanOrEqualTo(0);
            RuleFor(b => b.Limit)
                .InclusiveBetween(2, 1000);
        }
    }
}