using System;
using System.Security.Claims;

namespace ProjectManagementSystem.WebApi.Extensions
{
    internal static class EmployeesExtensions
    {
        public static Guid GetId(this ClaimsPrincipal claimsPrincipal)
        {
            return Guid.Parse(claimsPrincipal.FindFirst(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value);
        }

        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(x => x.Type.Equals(ClaimTypes.Email))?.Value;
        }

        public static string GetName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(x => x.Type.Equals(ClaimTypes.Name))?.Value;
        }
    }
}