using Microsoft.AspNetCore.Identity;
using ProjectManagementSystem.Domain.Admin.Users;

namespace ProjectManagementSystem.Infrastructure.PasswordHasher
{
    public sealed class PasswordHasher :
        Domain.Authentication.IPasswordHasher, 
        IPasswordHasher, 
        Domain.User.Accounts.IPasswordHasher
    {
        private sealed class User { }
        
        private readonly IPasswordHasher<User> _hasher = new PasswordHasher<User>();

        public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var passwordVerificationResult = _hasher.VerifyHashedPassword(new User(), hashedPassword, providedPassword);
            return passwordVerificationResult == PasswordVerificationResult.Success ||
                   passwordVerificationResult == PasswordVerificationResult.SuccessRehashNeeded;
        }

        public string HashPassword(string password)
        {
            return _hasher.HashPassword(new User(), password);        
        }
    }
}