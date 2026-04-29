using Vehicle.Insurance.Core.RequestResponse.Common.Requests;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.IssuanceScheme.GetAll;

namespace Vehicle.Insurance.Core.Contracts.CoreInsuranceApis.IssuanceSchemes;
public interface ICoreInsuranceGetAllIssuanceSchemesCaller
{
    Task<Response<List<GetAllIssuanceSchemesResponse>>> Call(GetAllIssuanceSchemesRequest request);
}

