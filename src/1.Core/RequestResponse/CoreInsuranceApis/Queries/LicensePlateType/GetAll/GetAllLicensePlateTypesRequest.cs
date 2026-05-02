using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.LicensePlateType.GetAll;

public sealed class GetAllLicensePlateTypesRequest : IWebRequest
{
    public string Path => "/noePelak/findByFilter";
}
