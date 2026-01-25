using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.InsuranceType.GetAll;

public sealed class GetAllInsuranceTypesRequest : IWebRequest
{
    public string Path => "/anvaBimeh/findByFilter";
}
