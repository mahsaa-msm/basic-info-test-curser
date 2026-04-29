using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.Province.GetAll;

public sealed class GetAllProvincesRequest : IWebRequest
{
    public string Path => "/ostan/findByFilter";
}

