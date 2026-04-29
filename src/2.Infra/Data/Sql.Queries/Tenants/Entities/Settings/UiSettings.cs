using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.Tenants.Entities.Settings;

public class UiSettings : TenantConfigSettings
{
    public override ConfigType Type => ConfigType.UI_STYLE_CONFIG;
    public string Theme { get; set; }
}
