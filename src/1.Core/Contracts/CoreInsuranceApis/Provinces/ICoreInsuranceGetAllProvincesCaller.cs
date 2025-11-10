using Master.Data.Core.RequestResponse.Common.Requests;
using Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.Province.GetAll;

namespace Master.Data.Core.Contracts.CoreInsuranceApis.Provinces;
public interface ICoreInsuranceGetAllProvincesCaller
{
    Task<Response<List<GetAllProvincesResponse>>> Call(GetAllProvincesRequest request);
}
