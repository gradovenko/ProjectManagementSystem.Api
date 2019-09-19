using System;
using FluentValidation;

namespace ProjectManagementSystem.WebApi.Models.Authentication
{
    public sealed class SignInBinding
    {
        /// <summary>
        /// Grant type
        /// </summary>
        public GrantType GrantType { get; set; }
        
        /// <summary>
        /// Login
        /// </summary>
        public string Login { get; set; }
        
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// Refresh token
        /// </summary>
        public Guid RefreshToken { get; set; }
    }

    public class SignInBingingValidator : AbstractValidator<SignInBinding>
    {
        public SignInBingingValidator()
        {
            RuleFor(b => b.GrantType)
                .IsInEnum();
            RuleFor(b => b.Login)
                .NotEmpty();
            RuleFor(b => b.Password)
                .NotEmpty();
            RuleFor(b => b.RefreshToken)
                .NotEmpty()
                .When(b => b.GrantType == GrantType.refresh_token);
        }
    }
}