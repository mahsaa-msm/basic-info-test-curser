using Vehicle.Insurance.Core.RequestResponse.Common.Requests;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.Agreements.GetAllAgreementObligations;

namespace Vehicle.Insurance.Core.Contracts.CoreInsuranceApis.Agreements;

public interface ICoreInsuranceGetAllAgreementObligationsCaller
{
    Task<Response<List<GetAllAgreementObligationsResponse>>> Call(GetAllAgreementObligationsRequest request);
}

