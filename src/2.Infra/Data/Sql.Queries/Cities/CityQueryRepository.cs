using Vehicle.Insurance.Core.Contracts.Cities.Queries;
using Vehicle.Insurance.Core.RequestResponse.Cities.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.Cities.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.Cities.Queries.GetById;
using Vehicle.Insurance.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Infra.Data.Sql.Queries;
using Zamin.Utilities.Extensions;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.Cities;

public sealed class CityQueryRepository : BaseQueryRepository<VehicleInsuranceQueryDbContext>,
    ICityQueryRepository
{
    public CityQueryRepository(VehicleInsuranceQueryDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<List<CitySelectItemQr>> Execute(GetAllCitiesQuery query)
        => await _dbContext.Cities
        .WhereIf(query.IsActive.HasValue, c => c.IsActive == query.IsActive)
        .WhereIf(query.ProvinceCoreId.HasValue,
                 c => c.ProvinceCoreId == query.ProvinceCoreId.ToString())
        .OrderBy(c => c.Priority)
        .Select(c => new CitySelectItemQr
        {
            CoreId = c.CoreId,
            DisplayTitle = c.DisplayTitle,
            ProvinceCoreId = c.ProvinceCoreId,
        }).ToListAsync();

    public async Task<PagedData<CityListItemQr>> Execute(GetAllCitiesPagedFilterQuery query)
    {
        var result = new PagedData<CityListItemQr>
        {
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };

        var filter = _dbContext.Cities.AsQueryable();

        if (!string.IsNullOrEmpty(query.CoreId))
            filter = filter.Where(c => c.CoreId == query.CoreId);

        if (!string.IsNullOrEmpty(query.Title))
            filter = filter.Where(c => c.Title.Contains(query.Title));

        if (!string.IsNullOrEmpty(query.DisplayTitle))
            filter = filter.Where(c => c.DisplayTitle.Contains(query.DisplayTitle));

        if (!string.IsNullOrEmpty(query.Code))
            filter = filter.Where(c => c.Code.Contains(query.Code));

        if (!string.IsNullOrEmpty(query.ProvinceCoreId))
            filter = filter.Where(c => c.ProvinceCoreId == query.ProvinceCoreId);

        if (query.IsActive != null)
            filter = filter.Where(c => c.IsActive == query.IsActive);

        if (query.Priority != null)
            filter = filter.Where(c => c.Priority == query.Priority);

        if (query.NeedTotalCount is not false)
            result.TotalCount = await filter.CountAsync();

        if (!string.IsNullOrWhiteSpace(query.SortBy))
            filter = filter.OrderByField(query.SortBy, query.SortAscending);

        filter = filter.Skip(query.SkipCount).Take(query.PageSize);

        result.QueryResult = await filter
            .Include(c => c.Province)
            .Select(c => new CityListItemQr
            {
                Id = c.Id,
                CoreId = c.CoreId,
                Title = c.Title,
                DisplayTitle = c.DisplayTitle,
                Code = c.Code,
                ProvinceCoreId = c.ProvinceCoreId,
                ProvinceDisplayTitle = c.Province != null ?
                    c.Province.DisplayTitle :
                    null,
                Priority = c.Priority,
                IsActive = c.IsActive,
            }).ToListAsync();

        return result;
    }

    public async Task<CityQr?> Execute(GetCityByIdQuery query)
         => await _dbContext.Cities
                .Select(c => new CityQr
                {
                    Id = c.Id,
                    CoreId = c.CoreId,
                    Title = c.Title,
                    DisplayTitle = c.DisplayTitle,
                    Code = c.Code,
                    ProvinceCoreId = c.ProvinceCoreId,
                    Priority = c.Priority,
                    IsActive = c.IsActive,
                    IsEditable = !string.IsNullOrEmpty(c.CreatedByUserId)

                })
                .FirstOrDefaultAsync(c => c.Id == query.CityId);
}
