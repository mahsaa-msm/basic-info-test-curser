using Vehicle.Insurance.Core.Contracts.InsuranceTypes.Queries;
using Vehicle.Insurance.Core.RequestResponse.InsuranceTypes.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.InsuranceTypes.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.InsuranceTypes.Queries.GetById;
using Vehicle.Insurance.Core.Resources;
using Vehicle.Insurance.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Infra.Data.Sql.Queries;
using Zamin.Utilities.Extensions;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.InsuranceTypes;

public sealed class InsuranceTypeQueryRepository : BaseQueryRepository<VehicleInsuranceQueryDbContext>,
    IInsuranceTypeQueryRepository
{
    public InsuranceTypeQueryRepository(VehicleInsuranceQueryDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<InsuranceTypeQr?> Execute(GetInsuranceTypeByIdQuery query)
         => await _dbContext.InsuranceTypes
                .Select(c => new InsuranceTypeQr
                {
                    Id = c.Id,
                    CoreId = c.CoreId,
                    ServiceFeatureCategory = c.ServiceFeatureCategory,
                    Title = c.Title,
                    DisplayTitle = c.DisplayTitle,
                    Code = c.Code,
                    Priority = c.Priority,
                    IsActive = c.IsActive,
                    IsEditable = !string.IsNullOrEmpty(c.CreatedByUserId)

                })
                .FirstOrDefaultAsync(c => c.Id == query.InsuranceTypeId);

    public async Task<List<InsuranceTypeSelectItemQr>> Execute(GetAllInsuranceTypeQuery query)
        => await _dbContext.InsuranceTypes
        .Where(c => !query.IsActive.HasValue || c.IsActive == query.IsActive)
        .OrderBy(c => c.Priority)
        .Select(c => new InsuranceTypeSelectItemQr
        {
            CoreId = c.CoreId,
            DisplayTitle = c.DisplayTitle,
        }).ToListAsync();

    public async Task<PagedData<InsuranceTypeListItemQr>> Execute(GetAllInsuranceTypesPagedFilterQuery query)
    {
        var filter = _dbContext.InsuranceTypes.AsQueryable();

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.CoreId),
                                c => c.CoreId == query.CoreId);

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.Code),
                        c => c.Code.Contains(query.Code!));

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.Title),
                        c => c.Title.Contains(query.Title!));

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.DisplayTitle),
                        c => c.DisplayTitle.Contains(query.DisplayTitle!));

        filter = filter.WhereIf(query.IsActive is not null,
                                c => c.IsActive == query.IsActive);

        filter = filter.WhereIf(query.ServiceFeatureCategory is not null,
                                c => c.ServiceFeatureCategory == query.ServiceFeatureCategory);

        filter = filter.WhereIf(query.Priority is not null,
                                c => c.Priority == query.Priority);

        return await filter.ToPagedData(query, c => new InsuranceTypeListItemQr
        {
            Id = c.Id,
            Title = c.Title,
            DisplayTitle = c.DisplayTitle,
            Code = c.Code,
            CoreId = c.CoreId,
            ServiceFeatureCategory = c.ServiceFeatureCategory,
            IsActive = c.IsActive,
            Priority = c.Priority,
        });
    }
}

