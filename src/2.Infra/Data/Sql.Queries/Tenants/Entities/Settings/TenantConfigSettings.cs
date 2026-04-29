using System.Text.Json.Serialization;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.Tenants.Entities.Settings;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "Type")]
[JsonDerivedType(typeof(SsoSettings), (int)ConfigType.SSO_CONFIG)]
[JsonDerivedType(typeof(PaymentSettings), (int)ConfigType.PAYMENT_CONFIG)]
[JsonDerivedType(typeof(UiSettings), (int)ConfigType.UI_STYLE_CONFIG)]
public abstract class TenantConfigSettings
{
    public abstract ConfigType Type { get; }
}

