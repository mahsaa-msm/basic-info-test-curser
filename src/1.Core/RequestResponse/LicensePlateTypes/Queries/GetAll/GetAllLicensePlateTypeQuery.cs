using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Queries.GetAll;

public sealed class GetAllLicensePlateTypeQuery : IQuery<List<LicensePlateTypeSelectItemQr>>, IWebRequest
{
    public bool? IsActive { get; set; }

    public string Path => "/Api/LicensePlateType/GetAllLicensePlateTypes";
}
