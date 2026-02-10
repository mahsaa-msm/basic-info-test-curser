using Zamin.Core.Domain.Entities;

namespace Master.Data.Core.Domain.Tenants.Entities;

public sealed class TenantConfigSettingsHistory : Entity
{
    public long TenantConfigId { get; private set; }
    public string? OldSettings { get; private set; }
    public string NewSettings { get; private set; }
    public DateTime ChangeDateUtc { get; private set; }

    private TenantConfigSettingsHistory()
    {
    }
    private TenantConfigSettingsHistory(string newSettings, string? oldSettings)
    {
        NewSettings = newSettings;
        ChangeDateUtc = DateTime.UtcNow;
        OldSettings = oldSettings;
    }

    #region Commands
    public static TenantConfigSettingsHistory Create(string newSettings)
        => new(newSettings, null);

    public static TenantConfigSettingsHistory Create(string newSettings, string oldSettings)
        => new(newSettings, oldSettings);

    #endregion
}
