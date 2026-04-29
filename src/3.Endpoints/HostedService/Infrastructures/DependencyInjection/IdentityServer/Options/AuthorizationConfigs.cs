namespace Vehicle.Insurance.Endpoints.HostedService.Infrastructures.DependencyInjection.IdentityServer.Options;

public class AuthorizationConfigs
{
    public bool Enabled { get; set; }
    public long ResourceId { get; set; }
    public string AclBaseUrl { get; set; }
    public string ApiToken { get; set; }
    public List<string> HeaderNamesToForceAuthorize { get; set; }
}
