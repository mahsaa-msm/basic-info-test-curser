using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Vehicle.Insurance.Core.RequestResponse.CoreSsoApis.Queries.GetTtoken;

public sealed class CoreSsoGetTokenResponse
{
    [JsonProperty("access_token")]
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = default!;

    [JsonProperty("token_type")]
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = default!;

    [JsonProperty("expires_in")]
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonProperty("scope")]
    [JsonPropertyName("scope")]
    public string Scope { get; set; } = default!;

    [JsonProperty("refresh_token")]
    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }

    [JsonProperty("iat")]
    [JsonPropertyName("iat")]
    public long? IssuedAt { get; set; }
}
