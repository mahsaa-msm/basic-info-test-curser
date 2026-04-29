using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.CommonResults;

public sealed class PatternCatalogQr
{
    public string Key { get; set; } = string.Empty;
    public string EncodedPattern { get; set; } = string.Empty;
    public PatternCatalogType Type { get; set; }
    public string? EncodedDescription { get; set; }
    public DateTime CreatedDateUtc { get; set; }
    public DateTime? LastModifiedDateUtc { get; set; }
    public long Priority { get; set; }
    public bool IsActive { get; set; }
}

