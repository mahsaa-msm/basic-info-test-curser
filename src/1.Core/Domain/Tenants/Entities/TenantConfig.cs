using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Domain.Tenants.Entities.Settings;
using Master.Data.Core.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using Zamin.Core.Domain.Entities;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.Domain.Tenants.Entities;

public sealed class TenantConfig : Entity
{
    #region Properties
    public long TenantId { get; private set; }
    public ConfigType ConfigType { get; private set; }
    public DateTime LastModifiedDateUtc { get; private set; }

    private readonly List<TenantConfigSettingsHistory> _tenantConfigSettingHistories = new();
    public IReadOnlyList<TenantConfigSettingsHistory> TenantConfigSettingHistories => _tenantConfigSettingHistories.AsReadOnly();

    private string _settingsJson;
    private TenantConfigSettings _settings;

    [NotMapped]
    public TenantConfigSettings Settings
    {
        get => _settings;
        set
        {
            _settings = value;
            _settingsJson = JsonConvert.SerializeObject(value, value.GetType(), new());
            ConfigType = value.Type;
        }
    }
    #endregion

    #region Constructors
    private TenantConfig()
    {
    }

    private TenantConfig(TenantConfigSettings settings)
    {
        Settings = settings;

        _tenantConfigSettingHistories.Add(TenantConfigSettingsHistory.Create(_settingsJson));

        Modified();
    }
    #endregion

    #region Commands
    public static TenantConfig Create(TenantConfigSettings settings)
        => new(settings);

    public void LoadSettings()
    {
        _settings = ConfigType switch
        {
            ConfigType.SSO_CONFIG => JsonConvert.DeserializeObject<SsoSettings>(_settingsJson),
            ConfigType.PAYMENT_CONFIG => JsonConvert.DeserializeObject<PaymentSettings>(_settingsJson),
            ConfigType.UI_STYLE_CONFIG => JsonConvert.DeserializeObject<UISettings>(_settingsJson),
            _ => throw new InvalidOperationException(string.Format(ProjectValidationError.VALIDATION_ERROR_FORMAT, ProjectTranslation.CONFIG_TYPE)),
        };
    }

    public void Update(TenantConfigSettings newSettings)
    {
        ValueObjectGuard.ThrowIfNull(newSettings, nameof(newSettings));

        ValueObjectGuard.ThrowIfNotValid(newSettings.Type == ConfigType, ProjectTranslation.CONFIG_TYPE);

        var oldSettings = _settingsJson;
        Settings = newSettings;
        _tenantConfigSettingHistories.Add(TenantConfigSettingsHistory.Create(oldSettings,
                                                                              _settingsJson));

        Modified();
    }

    private void Modified() => LastModifiedDateUtc = DateTime.UtcNow;
    #endregion
}
