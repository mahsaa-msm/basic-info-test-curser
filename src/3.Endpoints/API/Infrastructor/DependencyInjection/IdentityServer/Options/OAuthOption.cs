using System.Security.Claims;

namespace Master.Data.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer.Options;

public class OAuthOption
{
    public bool Enabled { get; set; } = true;
    public TokenType TokenType { get; set; } = TokenType.JWTToken;
    public string Authority { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public bool RequireHttpsMetadata { get; set; } = false;
    public bool IgnoreSSL { get; set; } = false;
    public Dictionary<string, string> Scopes { get; set; } = new Dictionary<string, string>();
    public bool ValidateAudience { get; set; } = false;
    public bool ValidateIssuer { get; set; } = false;
    public bool ValidateIssuerSigningKey { get; set; } = false;

    public RefrenceTokenConfigOption RefrenceTokenConfig { get; set; } = new();
    public RegisterUserInfoClaimsOption RegisterUserInfoClaims { get; set; } = new();
    public EndpointsPathOption EndpointsPath { get; set; } = null;
    public string UserIdentifierClaimType { get; set; } = ClaimTypes.NameIdentifier;
    public List<UserClaimRuleOption> UserClaimRules { get; set; } = [];
    public AuthorizationConfigs AuthorizationConfigs { get; set; }
    public FakeAuthOption FakeAuthOption { get; set; }
}
