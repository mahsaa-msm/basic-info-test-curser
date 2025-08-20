namespace Master.Data.Core.RequestResponse.Countries.Queries.GetAllPagedFilter;

public sealed class CountryListItemQr
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public int Priority { get; set; }
    public bool IsActive { get; set; }
}