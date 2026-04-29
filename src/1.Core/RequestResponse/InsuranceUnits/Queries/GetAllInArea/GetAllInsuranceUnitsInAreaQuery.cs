using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.RequestResponse.InsuranceUnits.Queries.GetAllInArea;

public sealed class GetAllInsuranceUnitsInAreaQuery : IQuery<List<InsuranceUnitMapItemQr>>, IWebRequest
{
    public AreaViewModel? Area { get; set; }
    public string? ProvinceCoreId { get; set; }
    public string? CityCoreId { get; set; }
    public InsuranceUnitType? Type { get; set; }
    public string? SearchInput { get; set; }
    public int MaxResults { get; set; } = 1000; // محدودیت برای performance

    public string Path => "/Api/InsuranceUnit/GetAllInsuranceUnitInArea";

    public class AreaViewModel
    {
        public double MinLatitude { get; set; }
        public double MinLongitude { get; set; }

        public double MaxLatitude { get; set; }
        public double MaxLongitude { get; set; }

        public bool IsValid() =>
            MinLatitude <= MaxLatitude &&
            MinLongitude <= MaxLongitude &&
            Math.Abs(MaxLatitude - MinLatitude) < 10.0 && // محدوده منطقی
            Math.Abs(MaxLongitude - MinLongitude) < 10.0;
    }
}

