using Microsoft.AspNetCore.Identity;

namespace ProjectManagementSystem.Infrastructure.PasswordHasher;

public sealed class PasswordHasher :
    Domain.Authentication.IPasswordHasher, 
    Domain.Users.IPasswordHasher
{
    private sealed class User { }
        
    private readonly IPasswordHasher<User> _hasher = new PasswordHasher<User>();

    public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        PasswordVerificationResult passwordVerificationResult = _hasher.VerifyHashedPassword(new User(), hashedPassword, providedPassword);
        return passwordVerificationResult is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded;
    }

    public string HashPassword(string password)
    {
        return _hasher.HashPassword(new User(), password);        
    }
}