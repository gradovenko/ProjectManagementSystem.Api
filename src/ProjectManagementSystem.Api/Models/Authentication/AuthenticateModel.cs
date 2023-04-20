using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementSystem.Api.Models.Authentication;

public record AuthenticateModel
{
    /// <summary>
    /// Grant type (password, refresh_token)
    /// </summary>
    [FromForm(Name = "grant_type")]
    public string GrantType { get; set; } = null!;

    /// <summary>
    /// Email or name (when grant type is password)
    /// </summary>
    [FromForm(Name = "username")]
    public string? UserName { get; set; }

    /// <summary>
    /// Password (when grant type is password)
    /// </summary>
    [FromForm(Name = "password")]
    public string? Password { get; set; }

    /// <summary>
    /// Refresh token (when grant type is refresh_token)
    /// </summary>
    [FromForm(Name = "refresh_token")]
    public string? RefreshToken { get; set; }
}