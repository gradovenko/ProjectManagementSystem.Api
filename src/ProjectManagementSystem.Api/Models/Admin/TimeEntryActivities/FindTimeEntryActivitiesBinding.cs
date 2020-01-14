using FluentValidation;

namespace ProjectManagementSystem.Api.Models.Admin.TimeEntryActivities
{
    public sealed class FindTimeEntryActivitiesBinding
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

    public sealed class FindTimeEntryActivitiesBindingValidator : AbstractValidator<FindTimeEntryActivitiesBinding>
    {
        public FindTimeEntryActivitiesBindingValidator()
        {
            RuleFor(b => b.Offset)
                .GreaterThanOrEqualTo(0);
            RuleFor(b => b.Limit)
                .InclusiveBetween(2, 1000);
        }
    }
}