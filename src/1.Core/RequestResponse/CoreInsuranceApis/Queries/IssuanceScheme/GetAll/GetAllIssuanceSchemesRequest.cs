using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.IssuanceScheme.GetAll;
public sealed class GetAllIssuanceSchemesRequest : IWebRequest
{
    public string Path => "/tarhSodoor/findByFilter";
}

