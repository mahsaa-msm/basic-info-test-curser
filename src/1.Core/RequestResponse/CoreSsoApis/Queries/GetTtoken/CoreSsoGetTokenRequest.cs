using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.CoreSsoApis.Queries.GetTtoken;
public sealed class CoreSsoGetTokenRequest : IQuery<CoreSsoGetTokenResponse>, IWebRequest
{
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public List<string>? Scopes { get; set; }
    public bool UseDefaultScopes { get; set; } = true;

    public const string NamePath = "token";
    public string Path => $"oauth/{NamePath}";
}