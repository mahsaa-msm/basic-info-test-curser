using Master.Data.Core.Domain.Tenants.Parameters;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.Domain.Tenants.Entities.Settings;

public sealed class UISettings : TenantConfigSettings
{
    public override ConfigType Type => ConfigType.UI_STYLE_CONFIG;

    public string Theme { get; private set; }

    private UISettings()
    {
    }
    private UISettings(CreateUiSettingsParameters parameters)
    {
        Theme = parameters.Theme;
    }

    public static UISettings Create(CreateUiSettingsParameters parameters)
        => new(parameters);
}