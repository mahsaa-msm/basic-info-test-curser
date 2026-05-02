using Vehicle.Insurance.Core.Contracts.VehicleTips.Queries;
using Vehicle.Insurance.Core.RequestResponse.VehicleTips.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.VehicleTips.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.VehicleTips.Queries.GetById;
using Vehicle.Insurance.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Infra.Data.Sql.Queries;
using Zamin.Utilities.Extensions;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.VehicleTips;

public sealed class VehicleTipQueryRepository : BaseQueryRepository<VehicleInsuranceQueryDbContext>,
    IVehicleTipQueryRepository
{
    public VehicleTipQueryRepository(VehicleInsuranceQueryDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<VehicleTipQr?> Execute(GetVehicleTipByIdQuery query)
        => await _dbContext.VehicleTips
            .Select(c => new VehicleTipQr
            {
                Id = c.Id,
                CoreId = c.CoreId,
                Title = c.Title,
                DisplayTitle = c.DisplayTitle,
                BrandCoreId = c.BrandCoreId,
                VehicleTypeCoreId = c.VehicleTypeCoreId,
                VehicleSystemCoreId = c.VehicleSystemCoreId,
                Priority = c.Priority,
                IsActive = c.IsActive,
                IsEditable = !string.IsNullOrEmpty(c.CreatedByUserId),
            })
            .FirstOrDefaultAsync(c => c.Id == query.VehicleTipId);

    public async Task<List<VehicleTipSelectItemQr>> Execute(GetAllVehicleTipQuery query)
        => await _dbContext.VehicleTips
            .Where(c => !query.IsActive.HasValue || c.IsActive == query.IsActive)
            .OrderBy(c => c.Priority)
            .Select(c => new VehicleTipSelectItemQr
            {
                CoreId = c.CoreId,
                DisplayTitle = c.DisplayTitle,
            }).ToListAsync();

    public async Task<PagedData<VehicleTipListItemQr>> Execute(GetAllVehicleTipsPagedFilterQuery query)
    {
        var filter = _dbContext.VehicleTips.AsQueryable();

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.CoreId), c => c.CoreId == query.CoreId);
        filter = filter.WhereIf(!string.IsNullOrEmpty(query.Title), c => c.Title.Contains(query.Title!));
        filter = filter.WhereIf(!string.IsNullOrEmpty(query.DisplayTitle),
            c => c.DisplayTitle.Contains(query.DisplayTitle!));
        filter = filter.WhereIf(!string.IsNullOrEmpty(query.BrandCoreId),
            c => c.BrandCoreId == query.BrandCoreId);
        filter = filter.WhereIf(!string.IsNullOrEmpty(query.VehicleTypeCoreId),
            c => c.VehicleTypeCoreId == query.VehicleTypeCoreId);
        filter = filter.WhereIf(!string.IsNullOrEmpty(query.VehicleSystemCoreId),
            c => c.VehicleSystemCoreId == query.VehicleSystemCoreId);
        filter = filter.WhereIf(query.IsActive is not null, c => c.IsActive == query.IsActive);
        filter = filter.WhereIf(query.Priority is not null, c => c.Priority == query.Priority);

        return await filter.ToPagedData(query, c => new VehicleTipListItemQr
        {
            Id = c.Id,
            Title = c.Title,
            DisplayTitle = c.DisplayTitle,
            CoreId = c.CoreId,
            BrandCoreId = c.BrandCoreId,
            VehicleTypeCoreId = c.VehicleTypeCoreId,
            VehicleSystemCoreId = c.VehicleSystemCoreId,
            IsActive = c.IsActive,
            Priority = c.Priority,
        });
    }
}
