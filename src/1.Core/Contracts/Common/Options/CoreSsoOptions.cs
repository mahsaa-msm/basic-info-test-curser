namespace Master.Data.Core.Contracts.Common.Options;
public sealed class CoreSsoOptions
{
    public string BasePath { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string GrantType { get; set; } = string.Empty;
    public bool IgnoreSslCheck { get; set; }
    public List<string>? Scopes { get; set; }
}
