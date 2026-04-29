namespace Vehicle.Insurance.Core.RequestResponse.Tenants.Queries.GetById.Dtos;

public sealed class PaymentConfigDto : ConfigDto
{
    public string PaymentGateway { get; set; } = string.Empty;
    public bool AllowRefund { get; set; }
}

