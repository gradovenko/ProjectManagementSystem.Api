using FluentValidation;

namespace ProjectManagementSystem.Api.Models.Admin.Trackers
{
    public sealed class FindTrackersBinding
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

    public sealed class FindTrackersBindingValidator : AbstractValidator<FindTrackersBinding>
    {
        public FindTrackersBindingValidator()
        {
            RuleFor(b => b.Offset)
                .GreaterThanOrEqualTo(0);
            RuleFor(b => b.Limit)
                .InclusiveBetween(2, 1000);
        }
    }
}