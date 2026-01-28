using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Infra.Data.Sql.Queries.Tenants.Entities.Settings;

public class PaymentSettings : TenantConfigSettings
{
    public override ConfigType Type => ConfigType.PAYMENT_CONFIG;
    public string PaymentGateway { get; set; }
    public bool AllowRefund { get; set; }
}