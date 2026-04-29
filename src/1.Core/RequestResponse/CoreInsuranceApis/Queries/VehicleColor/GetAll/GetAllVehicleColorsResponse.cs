namespace Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.VehicleColor.GetAll;

public sealed class GetAllVehicleColorsResponse
{
    public long rangBadanahID { get; set; }
    public string title { get; set; } = string.Empty;
    public string? displayTitle { get; set; }
    public string colorHash { get; set; } = string.Empty;
}

