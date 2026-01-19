using Master.Data.Core.RequestResponse.AgreementObligations.Queries.GetAll;
using Master.Data.Core.RequestResponse.AgreementObligations.Queries.GetAllPagedFilter;
using Master.Data.Core.RequestResponse.AgreementObligations.Queries.GetById;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.Contracts.AgreementObligations.Queries;

public interface IAgreementObligationQueryRepository : IQueryRepository
{
    Task<AgreementObligationQr?> ExecuteAsync(GetAgreementObligationByIdQuery query);
    Task<List<AgreementObligationSelectItemQr>> ExecuteAsync(GetAllAgreementObligationsQuery query);
    Task<PagedData<AgreementObligationListItemQr>> ExecuteAsync(GetAllAgreementObligationsPagedFilterQuery query);
}
