using Vehicle.Insurance.Core.RequestResponse.AgreementObligations.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.AgreementObligations.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.AgreementObligations.Queries.GetById;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.Contracts.AgreementObligations.Queries;

public interface IAgreementObligationQueryRepository : IQueryRepository
{
    Task<AgreementObligationQr?> ExecuteAsync(GetAgreementObligationByIdQuery query);
    Task<List<AgreementObligationSelectItemQr>> ExecuteAsync(GetAllAgreementObligationsQuery query);
    Task<PagedData<AgreementObligationListItemQr>> ExecuteAsync(GetAllAgreementObligationsPagedFilterQuery query);
}

