using Master.Data.Core.RequestResponse.Common.Requests;
using Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.Agreements.GetAllAgreementObligations;

namespace Master.Data.Core.Contracts.CoreInsuranceApis.Agreements;

public interface ICoreInsuranceGetAllAgreementObligationsCaller
{
    Task<Response<List<GetAllAgreementObligationsResponse>>> Call(GetAllAgreementObligationsRequest request);
}
