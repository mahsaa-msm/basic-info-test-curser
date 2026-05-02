using Vehicle.Insurance.Core.RequestResponse.Common.Requests;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.LicensePlateType.GetAll;

namespace Vehicle.Insurance.Core.Contracts.CoreInsuranceApis.LicensePlateTypes;

public interface ICoreInsuranceGetAllLicensePlateTypesCaller
{
    Task<Response<List<GetAllLicensePlateTypesResponseItem>>> Call(GetAllLicensePlateTypesRequest request);
}
