namespace Master.Data.Core.Contracts.TravelDurationTypess.Queries.CommonResult;

public class TravelDurationTypesQr
{
    public long Id { get; set; }
    public int CoreId { get; set; }
    public required string Title { get; set; }
    public long Priority { get; set; }
    public bool IsEnable { get; set; }
}
