using Vehicle.Insurance.Core.Contracts.VehicleBrands.Queries;
using Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Queries.GetById;
using Vehicle.Insurance.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Infra.Data.Sql.Queries;
using Zamin.Utilities.Extensions;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.VehicleBrands;

public sealed class VehicleBrandQueryRepository : BaseQueryRepository<VehicleInsuranceQueryDbContext>,
    IVehicleBrandQueryRepository
{
    public VehicleBrandQueryRepository(VehicleInsuranceQueryDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<VehicleBrandQr?> Execute(GetVehicleBrandByIdQuery query)
        => await _dbContext.VehicleBrands
            .Select(c => new VehicleBrandQr
            {
                Id = c.Id,
                CoreId = c.CoreId,
                Title = c.Title,
                DisplayTitle = c.DisplayTitle,
                Priority = c.Priority,
                IsActive = c.IsActive,
                IsEditable = !string.IsNullOrEmpty(c.CreatedByUserId),
            })
            .FirstOrDefaultAsync(c => c.Id == query.VehicleBrandId);

    public async Task<List<VehicleBrandSelectItemQr>> Execute(GetAllVehicleBrandQuery query)
        => await _dbContext.VehicleBrands
            .Where(c => !query.IsActive.HasValue || c.IsActive == query.IsActive)
            .OrderBy(c => c.Priority)
            .Select(c => new VehicleBrandSelectItemQr
            {
                CoreId = c.CoreId,
                DisplayTitle = c.DisplayTitle,
            }).ToListAsync();

    public async Task<PagedData<VehicleBrandListItemQr>> Execute(GetAllVehicleBrandsPagedFilterQuery query)
    {
        var filter = _dbContext.VehicleBrands.AsQueryable();

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.CoreId), c => c.CoreId == query.CoreId);
        filter = filter.WhereIf(!string.IsNullOrEmpty(query.Title), c => c.Title.Contains(query.Title!));
        filter = filter.WhereIf(!string.IsNullOrEmpty(query.DisplayTitle),
            c => c.DisplayTitle.Contains(query.DisplayTitle!));
        filter = filter.WhereIf(query.IsActive is not null, c => c.IsActive == query.IsActive);
        filter = filter.WhereIf(query.Priority is not null, c => c.Priority == query.Priority);

        return await filter.ToPagedData(query, c => new VehicleBrandListItemQr
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
