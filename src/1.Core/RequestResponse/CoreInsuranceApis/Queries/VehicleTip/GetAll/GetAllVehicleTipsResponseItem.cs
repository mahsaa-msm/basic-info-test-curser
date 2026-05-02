namespace Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.VehicleTip.GetAll;

public sealed class GetAllVehicleTipsResponseItem
{
    public int TipID { get; set; }
    public string Tip { get; set; } = string.Empty;
    public int SystemID { get; set; }
    public int BrandID { get; set; }
    public string Brand { get; set; } = string.Empty;
    public int NoeVasilehID { get; set; }
}
