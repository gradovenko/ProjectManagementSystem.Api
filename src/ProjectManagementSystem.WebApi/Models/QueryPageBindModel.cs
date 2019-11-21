using FluentValidation;

namespace ProjectManagementSystem.WebApi.Models
{
    public class QueryPageBindModel
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
    
    public sealed class QueryPageBindModelValidator : AbstractValidator<QueryPageBindModel>
    {
        public QueryPageBindModelValidator()
        {
            RuleFor(b => b.Offset)
                .GreaterThanOrEqualTo(0);
            RuleFor(b => b.Limit)
                .InclusiveBetween(2, 1000);
        }
    }
}