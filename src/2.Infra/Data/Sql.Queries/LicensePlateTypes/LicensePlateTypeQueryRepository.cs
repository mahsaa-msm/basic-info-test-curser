using Vehicle.Insurance.Core.Contracts.LicensePlateTypes.Queries;
using Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Queries.GetById;
using Vehicle.Insurance.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Infra.Data.Sql.Queries;
using Zamin.Utilities.Extensions;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.LicensePlateTypes;

public sealed class LicensePlateTypeQueryRepository : BaseQueryRepository<VehicleInsuranceQueryDbContext>,
    ILicensePlateTypeQueryRepository
{
    public LicensePlateTypeQueryRepository(VehicleInsuranceQueryDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<LicensePlateTypeQr?> Execute(GetLicensePlateTypeByIdQuery query)
        => await _dbContext.LicensePlateTypes
            .Select(c => new LicensePlateTypeQr
            {
                Id = c.Id,
                CoreId = c.CoreId,
                Title = c.Title,
                DisplayTitle = c.DisplayTitle,
                Priority = c.Priority,
                IsActive = c.IsActive,
                IsEditable = !string.IsNullOrEmpty(c.CreatedByUserId),
            })
            .FirstOrDefaultAsync(c => c.Id == query.LicensePlateTypeId);

    public async Task<List<LicensePlateTypeSelectItemQr>> Execute(GetAllLicensePlateTypeQuery query)
        => await _dbContext.LicensePlateTypes
            .Where(c => !query.IsActive.HasValue || c.IsActive == query.IsActive)
            .OrderBy(c => c.Priority)
            .Select(c => new LicensePlateTypeSelectItemQr
            {
                CoreId = c.CoreId,
                DisplayTitle = c.DisplayTitle,
            }).ToListAsync();

    public async Task<PagedData<LicensePlateTypeListItemQr>> Execute(GetAllLicensePlateTypesPagedFilterQuery query)
    {
        var filter = _dbContext.LicensePlateTypes.AsQueryable();

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.CoreId), c => c.CoreId == query.CoreId);
        filter = filter.WhereIf(!string.IsNullOrEmpty(query.Title), c => c.Title.Contains(query.Title!));
        filter = filter.WhereIf(!string.IsNullOrEmpty(query.DisplayTitle),
            c => c.DisplayTitle.Contains(query.DisplayTitle!));
        filter = filter.WhereIf(query.IsActive is not null, c => c.IsActive == query.IsActive);
        filter = filter.WhereIf(query.Priority is not null, c => c.Priority == query.Priority);

        return await filter.ToPagedData(query, c => new LicensePlateTypeListItemQr
        {
            Id = c.Id,
            Title = c.Title,
            DisplayTitle = c.DisplayTitle,
            CoreId = c.CoreId,
            IsActive = c.IsActive,
            Priority = c.Priority,
        });
    }
}
