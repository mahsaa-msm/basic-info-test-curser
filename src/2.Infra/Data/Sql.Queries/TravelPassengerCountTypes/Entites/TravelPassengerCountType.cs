namespace Master.Data.Infra.Data.Sql.Queries.TravelPassengerCountTypes.Entites;
public partial class TravelPassengerCountType
{
    public int Id { get; set; }
    public int CoreId { get; set; }
    public string Title { get; set; }
    public long Priority { get; set; }
    public bool IsEnable { get; set; }
    public string? CreatedByUserId { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public string? ModifiedByUserId { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
    public Guid BusinessId { get; set; }
}
