using Master.Data.Core.RequestResponse.Tenants.Queries.GetById.Dtos;

namespace Master.Data.Core.RequestResponse.Tenants.Queries.GetById;

public sealed class TenantGraphQr
{
    public long Id { get; set; }
    public Guid TenantKey { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedDateUtc { get; set; }
    public List<object> Configs { get; set; } = new();
    public SsoConfigDto? SsoConfig { get; set; }
    public PaymentConfigDto? PaymentConfig { get; set; }
    public UiConfigDto? UIConfig { get; set; }
}
