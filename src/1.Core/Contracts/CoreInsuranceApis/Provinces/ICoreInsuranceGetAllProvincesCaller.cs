using Vehicle.Insurance.Core.RequestResponse.Common.Requests;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.Province.GetAll;

namespace Vehicle.Insurance.Core.Contracts.CoreInsuranceApis.Provinces;

public interface ICoreInsuranceGetAllProvincesCaller
{
    Task<Response<List<GetAllProvincesResponse>>> Call(GetAllProvincesRequest request);
}

