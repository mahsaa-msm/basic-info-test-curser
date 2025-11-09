namespace Master.Data.Endpoints.HostedService.Infrastructures.DependencyInjection.IdentityServer.Options;

public class UserClaimRuleOption
{
    public string Source { get; set; } = default!;
    public string Destination { get; set; } = default!;
    public bool RemoveSource { get; set; } = false;
}