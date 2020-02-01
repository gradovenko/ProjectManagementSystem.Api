using System.Text.Json.Serialization;

namespace ProjectManagementSystem.Api.Models.Authentication
{
    public sealed class ErrorView
    {
        /// <summary>
        /// A single ASCII error code
        /// https://tools.ietf.org/html/rfc6749#section-5.2
        /// </summary>
        [JsonPropertyName("error")]
        public string Error { get; }

        /// <summary>
        /// Human-readable ASCII text providing
        /// additional information, used to assist the client developer in
        /// understanding the error that occurred.
        /// https://tools.ietf.org/html/rfc6749#section-5.2
        /// </summary>
        [JsonPropertyName("error_description")]
        public string ErrorDescription { get; }
        
        public ErrorView(string error, string errorDescription)
        {
            Error = error;
            ErrorDescription = errorDescription;
        }
    }
}