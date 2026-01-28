using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.IssuanceScheme.GetAll;
public sealed class GetAllIssuanceSchemesRequest : IWebRequest
{
    public string Path => "/tarhSodoor/findByFilter";
}
