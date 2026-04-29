using Vehicle.Insurance.Core.RequestResponse.Common.Requests;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.InsuranceType.GetAll;

namespace Vehicle.Insurance.Core.Contracts.CoreInsuranceApis.InsuranceTypes;

public interface ICoreInsuranceGetAllInsuranceTypesCaller
{
    Task<Response<List<GetAllInsuranceTypesResponse>>> Call(GetAllInsuranceTypesRequest request);
}

