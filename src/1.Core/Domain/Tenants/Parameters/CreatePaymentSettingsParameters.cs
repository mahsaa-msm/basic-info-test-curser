namespace Vehicle.Insurance.Core.Domain.Tenants.Parameters;

public sealed record CreatePaymentSettingsParameters(string PaymentGateway,
                                                     bool AllowRefund);
