using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.Tenants.Entities.Settings;

public class SsoSettings : TenantConfigSettings
{
    public override ConfigType Type => ConfigType.SSO_CONFIG;
    public string SsoBasePath { get; set; }
    public string UserName { get; set; }
    public string OauthType { get; set; }
}
