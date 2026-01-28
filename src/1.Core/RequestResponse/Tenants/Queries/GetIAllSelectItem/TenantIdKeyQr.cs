namespace Master.Data.Core.RequestResponse.Tenants.Queries.GetIAllSelectItem;

public sealed class TenantIdKeyQr
{
    public long Id { get; set; }
    public Guid Key { get; set; }
    public string Name { get; set; } = string.Empty;
}
