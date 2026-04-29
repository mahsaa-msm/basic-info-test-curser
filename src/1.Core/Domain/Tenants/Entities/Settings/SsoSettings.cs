using Vehicle.Insurance.Core.Domain.Tenants.Parameters;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.Domain.Tenants.Entities.Settings;

public sealed class SsoSettings : TenantConfigSettings
{
    public override ConfigType Type => ConfigType.SSO_CONFIG;

    public string SsoBasePath { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; }
    public string OauthType { get; private set; }

    private SsoSettings()
    {
    }

    private SsoSettings(CreateSsoSettingsParameters parameters)
    {
        SsoBasePath = parameters.SsoBasePath;
        UserName = parameters.UserName;
        Password = parameters.Password;
        OauthType = parameters.OauthType;
    }

    public static SsoSettings Create(CreateSsoSettingsParameters parameters)
        => new(parameters);
}

