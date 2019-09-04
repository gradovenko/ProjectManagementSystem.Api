using Microsoft.AspNetCore.Identity;
using ProjectManagementSystem.Domain.Admin.CreateUsers;

namespace ProjectManagementSystem.Infrastructure.PasswordHasher
{
    public class PasswordHasher : Domain.Authentication.IPasswordHasher, IPasswordHasher
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