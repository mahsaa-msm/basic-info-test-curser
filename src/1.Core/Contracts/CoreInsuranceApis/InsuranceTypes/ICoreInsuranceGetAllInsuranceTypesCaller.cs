using Master.Data.Core.RequestResponse.Common.Requests;
using Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.InsuranceType.GetAll;

namespace Master.Data.Core.Contracts.CoreInsuranceApis.InsuranceTypes;
public interface ICoreInsuranceGetAllInsuranceTypesCaller
{
    Task<Response<List<GetAllInsuranceTypesResponse>>> Call(GetAllInsuranceTypesRequest request);
}
