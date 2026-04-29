using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.Tenants.Entities.Settings;

public class PaymentSettings : TenantConfigSettings
{
    public override ConfigType Type => ConfigType.PAYMENT_CONFIG;
    public string PaymentGateway { get; set; }
    public bool AllowRefund { get; set; }
}
