using System.Text.Json.Serialization;

namespace ProjectManagementSystem.Api.Models.Authentication;

public class TokenView
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; }
        
    [JsonPropertyName("token_type")]
    public string TokenType { get; }
        
    [JsonPropertyName("expires_in")]
    public long ExpiresIn { get; }
        
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; }

    public TokenView(string accessToken, TimeSpan expiresIn, string refreshToken)
    {
        AccessToken = accessToken;
        TokenType = "Bearer";
        ExpiresIn = (long)expiresIn.TotalSeconds;
        RefreshToken = refreshToken;
    }
}