using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementSystem.Api.Models.Authentication;

public sealed class AuthenticationBinding
{
    /// <summary>
    /// Grant type
    /// </summary>
    [FromForm(Name = "grant_type")]
    public string GrantType { get; set; }
        
    /// <summary>
    /// Username
    /// </summary>
    [FromForm(Name = "username")]
    public string Username { get; set; }
        
    /// <summary>
    /// Password
    /// </summary>
    [FromForm(Name = "password")]
    public string Password { get; set; }
        
    /// <summary>
    /// Refresh token
    /// </summary>
    [FromForm(Name = "refresh_token")]
    public string RefreshToken { get; set; }
}