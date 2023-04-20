using System.Text.Json.Serialization;

namespace ProjectManagementSystem.Api.Models.Authentication;

public sealed record ErrorViewModel(ErrorCode Error, string ErrorDescription)
{
    /// <summary>
    /// Error code https://tools.ietf.org/html/rfc6749#section-5.2
    /// </summary>
    [JsonPropertyName("error")]
    public ErrorCode Error { get; } = Error;

    /// <summary>
    /// Human-readable text providing additional information
    /// </summary>
    [JsonPropertyName("error_description")]
    public string ErrorDescription { get; } = ErrorDescription;
}