namespace Master.Data.Core.RequestResponse.PatternCatalogs.Queries.CommonResults;

public sealed class PatternCatalogQr
{
    public string Key { get; set; }
    public string Pattern { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDateUtc { get; set; }
    public DateTime? LastModifiedDateUtc { get; set; }
    public long Priority { get; set; }
    public bool IsActive { get; set; }
}
