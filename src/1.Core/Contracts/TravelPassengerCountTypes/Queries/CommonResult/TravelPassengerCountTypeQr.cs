namespace Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.CommonResult;

public class TravelPassengerCountTypeQr
{
    public int Id { get; set; }
    public int CoreId { get; set; }
    public required string Title { get; set; }
    public long Priority { get; set; }
    public bool IsEnable { get; set; }
}
