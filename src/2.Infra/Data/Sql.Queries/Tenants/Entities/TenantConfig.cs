using Vehicle.Insurance.Infra.Data.Sql.Queries.Common.Entites;
using Vehicle.Insurance.Infra.Data.Sql.Queries.Tenants.Entities.Settings;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.Tenants.Entities;

public sealed class TenantConfig : QueryObject
{
    public long TenantId { get; set; }
    public ConfigType ConfigType { get; set; }
    public DateTime LastModifiedDateUtc { get; set; }
    public string SettingsJson { get; set; }

    [NotMapped]
    public TenantConfigSettings Settings
    {
        get => DeserializeSettings(SettingsJson);
        set => SettingsJson = SerializeSettings(value);
    }
    public List<TenantConfigSettingsHistory> TenantConfigSettingHistories { get; set; } = new();

    public Tenant Tenant { get; set; } = new();

    private static TenantConfigSettings DeserializeSettings(string json)
    {
        if (string.IsNullOrEmpty(json)) return null;

        using var doc = JsonDocument.Parse(json);
        if (!doc.RootElement.TryGetProperty("Type", out var typeElement))
            throw new InvalidOperationException("Missing type discriminator");

        var type = (ConfigType)typeElement.GetInt32();

        return type switch
        {
            ConfigType.SSO_CONFIG => JsonSerializer.Deserialize<SsoSettings>(json),
            ConfigType.PAYMENT_CONFIG => JsonSerializer.Deserialize<PaymentSettings>(json),
            ConfigType.UI_STYLE_CONFIG => JsonSerializer.Deserialize<UiSettings>(json),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Unknown config type")
        };
    }

    private static string SerializeSettings(TenantConfigSettings settings)
    {
        if (settings == null) return null;
        return JsonSerializer.Serialize(settings, settings.GetType(), new JsonSerializerOptions());
    }
}

