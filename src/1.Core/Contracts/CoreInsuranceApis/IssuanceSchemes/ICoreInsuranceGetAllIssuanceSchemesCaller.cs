using Master.Data.Core.RequestResponse.Common.Requests;
using Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.IssuanceScheme.GetAll;

namespace Master.Data.Core.Contracts.CoreInsuranceApis.IssuanceSchemes;
public interface ICoreInsuranceGetAllIssuanceSchemesCaller
{
    Task<Response<List<GetAllIssuanceSchemesResponse>>> Call(GetAllIssuanceSchemesRequest request);
}
