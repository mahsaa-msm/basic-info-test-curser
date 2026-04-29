using Vehicle.Insurance.Core.RequestResponse.InsuranceUnits.Queries.GetAllInArea;
using Vehicle.Insurance.Core.RequestResponse.InsuranceUnits.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.InsuranceUnits.Queries.GetById;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.Contracts.InsuranceUnits.Queries;

public interface IInsuranceUnitQueryRepository : IQueryRepository
{
    Task<InsuranceUnitQr> Execute(GetInsuranceUnitByIdQuery query);
    Task<List<InsuranceUnitMapItemQr>> Execute(GetAllInsuranceUnitsInAreaQuery query);
    Task<PagedData<InsuranceUnitListItemQr>> Execute(GetAllInsuranceUnitsPagedFilterQuery query);
}

