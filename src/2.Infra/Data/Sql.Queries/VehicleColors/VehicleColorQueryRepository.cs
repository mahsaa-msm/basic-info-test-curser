using Vehicle.Insurance.Core.Contracts.VehicleColors.Queries;
using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Queries.GetById;
using Vehicle.Insurance.Core.Resources;
using Vehicle.Insurance.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Infra.Data.Sql.Queries;
using Zamin.Utilities.Extensions;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.VehicleColors;

public sealed class VehicleColorQueryRepository : BaseQueryRepository<VehicleInsuranceQueryDbContext>,
    IVehicleColorQueryRepository
{
    public VehicleColorQueryRepository(VehicleInsuranceQueryDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<VehicleColorQr?> Execute(GetVehicleColorByIdQuery query)
         => await _dbContext.VehicleColors
                .Select(c => new VehicleColorQr
                {
                    Id = c.Id,
                    CoreId = c.CoreId,
                    Title = c.Title,
                    DisplayTitle = c.DisplayTitle,
                    ColorHash = c.ColorHash,
                    Priority = c.Priority,
                    IsActive = c.IsActive,
                    IsEditable = !string.IsNullOrEmpty(c.CreatedByUserId)

                })
                .FirstOrDefaultAsync(c => c.Id == query.VehicleColorId);

    public async Task<List<VehicleColorSelectItemQr>> Execute(GetAllVehicleColorQuery query)
        => await _dbContext.VehicleColors
        .Where(c => !query.IsActive.HasValue || c.IsActive == query.IsActive)
        .OrderBy(c => c.Priority)
        .Select(c => new VehicleColorSelectItemQr
        {
            CoreId = c.CoreId,
            DisplayTitle = c.DisplayTitle,
        }).ToListAsync();

    public async Task<PagedData<VehicleColorListItemQr>> Execute(GetAllVehicleColorsPagedFilterQuery query)
    {
        var filter = _dbContext.VehicleColors.AsQueryable();

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.CoreId),
                                c => c.CoreId == query.CoreId);

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.ColorHash),
                        c => c.ColorHash.Contains(query.ColorHash!));

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.Title),
                        c => c.Title.Contains(query.Title!));

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.DisplayTitle),
                        c => c.DisplayTitle.Contains(query.DisplayTitle!));

        filter = filter.WhereIf(query.IsActive is not null,
                                c => c.IsActive == query.IsActive);

        filter = filter.WhereIf(query.Priority is not null,
                                c => c.Priority == query.Priority);

        return await filter.ToPagedData(query, c => new VehicleColorListItemQr
        {
            Id = c.Id,
            Title = c.Title,
            DisplayTitle = c.DisplayTitle,
            ColorHash = c.ColorHash,
            CoreId = c.CoreId,
            IsActive = c.IsActive,
            Priority = c.Priority,
        });
    }
}




