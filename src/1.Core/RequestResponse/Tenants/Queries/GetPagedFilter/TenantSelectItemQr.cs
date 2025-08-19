namespace Master.Data.Core.RequestResponse.Tenants.Queries.GetPagedFilter;
public sealed class TenantSelectItemQr
{
    public long Id { get; set; }
    public Guid TenantKey { get; set; }
    public string Name { get; set; } = default!;
    public bool IsActive { get; set; }
    public bool HasUiConfig { get; set; }
    public bool HasSsoConfig { get; set; }
    public bool HasPaymentConfig { get; set; }
}
