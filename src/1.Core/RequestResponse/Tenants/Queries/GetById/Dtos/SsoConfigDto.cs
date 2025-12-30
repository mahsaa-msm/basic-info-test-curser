namespace Master.Data.Core.RequestResponse.Tenants.Queries.GetById.Dtos;

public sealed class SsoConfigDto : ConfigDto
{
    public string SsoBasePath { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string OauthType { get; set; } = string.Empty;
}
