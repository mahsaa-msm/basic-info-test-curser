using System.Text.Json.Serialization;

namespace Master.Data.Core.Contracts.ExternalAPI.Authentication.Response;


public class GetTokenSSOResponseModel
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }

    [JsonPropertyName("expires_in")]
    public long ExpiresIn { get; set; }

    [JsonPropertyName("scope")]
    public string Scope { get; set; }

    [JsonPropertyName("ip")]
    public string Ip { get; set; }

    [JsonPropertyName("iat")]
    public long IssuedAt { get; set; }
}



