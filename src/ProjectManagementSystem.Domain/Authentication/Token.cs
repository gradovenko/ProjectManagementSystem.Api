using System;

namespace ProjectManagementSystem.Domain.Authentication
{
    public sealed class Token
    {
        public string AccessToken { get; }

        public TimeSpan ExpiresIn { get; }

        public Guid RefreshToken { get; }
        
        public Token(string accessToken, TimeSpan expiresIn, Guid refreshToken)
        {
            AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            ExpiresIn = expiresIn;
            RefreshToken = refreshToken;
        }
    }
}