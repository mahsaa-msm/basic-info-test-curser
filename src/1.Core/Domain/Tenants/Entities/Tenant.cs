using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.Tenants.Entities.Settings;
using Vehicle.Insurance.Core.Domain.Tenants.ValueObjects;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.Domain.Entities;
using Zamin.Core.Domain.ValueObjects;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.Domain.Tenants.Entities;

public sealed class Tenant : AggregateRoot
{
    #region Properties
    public BusinessId TenantKey { get; private set; }
    public DIPTitle Name { get; private set; }
    public TenantSlug Slug { get; private set; }
    public IsActive IsActive { get; private set; }
    public DateTime CreatedDateUtc { get; private set; }

    private readonly List<TenantConfig> _configs = new();
    public IReadOnlyList<TenantConfig> Configs => _configs.AsReadOnly();
    #endregion

    #region Constructors
    private Tenant()
    {
    }

    private Tenant(DIPTitle name, TenantSlug slug)
    {
        Name = name;
        Slug = slug;
        IsActive = IsActive.True();
        CreatedDateUtc = DateTime.UtcNow;
        TenantKey = BusinessId.FromGuid(Guid.NewGuid());
    }
    #endregion

    #region Commands
    public static Tenant Create(DIPTitle name, TenantSlug slug) => new(name, slug);

    public void Activate() => IsActive = IsActive.True();

    public void Deactivate() => IsActive = IsActive.False();

    public void UpdateName(DIPTitle name) => Name = name;

    public void AddConfig(TenantConfigSettings settings)
    {
        ValueObjectGuard.ThrowIfNull(settings, nameof(settings));
        ValueObjectGuard.ThrowIfNotValid(!ConfigExists(settings.Type), ProjectTranslation.CONFIG_TYPE);

        var tenantConfig = TenantConfig.Create(settings);
        _configs.Add(tenantConfig);
    }

    public void UpdateConfig(TenantConfigSettings newSettings)
    {
        ValueObjectGuard.ThrowIfNull(newSettings, nameof(newSettings));

        var existingConfig = GetConfig(newSettings.Type);
        EntityGuard.ThrowIfNull<TenantConfig, long>(existingConfig, ProjectTranslation.TENANT_CONFIG);

        existingConfig.LoadSettings();

        existingConfig.Update(newSettings);
    }

    public void RemoveConfig(ConfigType configType)
    {
        var configToRemove = GetConfig(configType);
        EntityGuard.ThrowIfNull<TenantConfig, long>(configToRemove, ProjectTranslation.TENANT_CONFIG);

        _configs.Remove(configToRemove);
    }
    #endregion

    #region Queries
    public TenantConfig? GetConfig(ConfigType configType) =>
        _configs.FirstOrDefault(c => c.ConfigType == configType);

    public T? GetConfigSettings<T>() where T : TenantConfigSettings
    {
        var configType = GetConfigTypeFromGeneric<T>();
        var config = GetConfig(configType);

        if (config is null) return default;

        config.LoadSettings();
        return config.Settings as T;
    }
    #endregion

    #region Helpers
    public bool ConfigExists(ConfigType configType) =>
        _configs.Any(c => c.ConfigType == configType);

    private ConfigType GetConfigTypeFromGeneric<T>() where T : TenantConfigSettings
    {
        // ایجاد یک نمونه موقت برای دریافت نوع کانفیگ
        var tempInstance = Activator.CreateInstance<T>();
        return tempInstance.Type;
    }
    #endregion
}

