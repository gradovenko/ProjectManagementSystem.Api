using System;

namespace ProjectManagementSystem.Domain.User.Accounts
{
    public sealed class EmailAlreadyExistsException : Exception
    {
        public EmailAlreadyExistsException() { }
    }
}