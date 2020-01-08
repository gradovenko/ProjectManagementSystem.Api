using FluentValidation;

namespace ProjectManagementSystem.WebApi.Models.User.ProjectTimeEntries
{
    public sealed class FindTimeEntriesBinding
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

    public sealed class FindTimeEntriesBindingValidator : AbstractValidator<FindTimeEntriesBinding>
    {
        public FindTimeEntriesBindingValidator()
        {
            RuleFor(b => b.Offset)
                .GreaterThanOrEqualTo(0);
            RuleFor(b => b.Limit)
                .InclusiveBetween(2, 1000);
        }
    }
}