using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Infra.Data.Sql.Queries.Tenants.Entities.Settings;

public class UiSettings : TenantConfigSettings
{
    public override ConfigType Type => ConfigType.UI_STYLE_CONFIG;
    public string Theme { get; set; }
}