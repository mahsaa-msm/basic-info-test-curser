using Master.Data.Core.RequestResponse.InsuranceTypes.Queries.GetAll;
using Master.Data.Core.RequestResponse.InsuranceTypes.Queries.GetAllPagedFilter;
using Master.Data.Core.RequestResponse.InsuranceTypes.Queries.GetById;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.Contracts.InsuranceTypes.Queries;

public interface IInsuranceTypeQueryRepository : IQueryRepository
{
    Task<InsuranceTypeQr> Execute(GetInsuranceTypeByIdQuery query);
    Task<List<InsuranceTypeSelectItemQr>> Execute(GetAllInsuranceTypeQuery query);
    Task<PagedData<InsuranceTypeListItemQr>> Execute(GetAllInsuranceTypesPagedFilterQuery query);
}
