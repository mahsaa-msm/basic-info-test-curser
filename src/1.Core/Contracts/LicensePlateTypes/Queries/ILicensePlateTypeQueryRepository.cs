using Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Queries.GetById;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.Contracts.LicensePlateTypes.Queries;

public interface ILicensePlateTypeQueryRepository : IQueryRepository
{
    Task<LicensePlateTypeQr?> Execute(GetLicensePlateTypeByIdQuery query);
    Task<List<LicensePlateTypeSelectItemQr>> Execute(GetAllLicensePlateTypeQuery query);
    Task<PagedData<LicensePlateTypeListItemQr>> Execute(GetAllLicensePlateTypesPagedFilterQuery query);
}
