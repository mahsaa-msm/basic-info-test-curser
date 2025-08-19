using Master.Data.Core.Domain.Tenants.Parameters;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.Domain.Tenants.Entities.Settings;

public sealed class PaymentSettings : TenantConfigSettings
{
    public override ConfigType Type => ConfigType.PAYMENT_CONFIG;

    public string PaymentGateway { get; private set; }
    public bool AllowRefund { get; private set; }

    private PaymentSettings()
    {
    }
    private PaymentSettings(CreatePaymentSettingsParameters parameters)
    {
        PaymentGateway = parameters.PaymentGateway;
        AllowRefund = parameters.AllowRefund;
    }

    public static PaymentSettings Create(CreatePaymentSettingsParameters parameters)
        => new(parameters);
}
