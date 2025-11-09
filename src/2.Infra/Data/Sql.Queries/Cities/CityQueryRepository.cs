using Master.Data.Core.Contracts.Cities.Queries;
using Master.Data.Core.Contracts.Provinces.Queries;
using Master.Data.Core.RequestResponse.Cities.Queries.GetAll;
using Master.Data.Core.RequestResponse.Cities.Queries.GetAllPagedFilter;
using Master.Data.Core.RequestResponse.Cities.Queries.GetById;
using Master.Data.Core.RequestResponse.Provinces.Queries.GetAll;
using Master.Data.Core.RequestResponse.Provinces.Queries.GetAllPagedFilter;
using Master.Data.Core.RequestResponse.Provinces.Queries.GetById;
using Master.Data.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Infra.Data.Sql.Queries;
using Zamin.Utilities.Extensions;

namespace Master.Data.Infra.Data.Sql.Queries.Cities;
public sealed class CityQueryRepository : BaseQueryRepository<MasterDataQueryDbContext>,
    ICityQueryRepository
{
    public CityQueryRepository(MasterDataQueryDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<List<CitySelectItemQr>> Execute(GetAllCitiesQuery query)
        => await _dbContext.Cities
        .Where(c => !query.IsActive.HasValue || c.IsActive == query.IsActive)
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

        result.QueryResult = await filter.Select(c => new CityListItemQr
        {
            Id = c.Id,
            CoreId = c.CoreId,
            Title = c.Title,
            DisplayTitle = c.DisplayTitle,
            Code = c.Code,
            ProvinceCoreId = c.ProvinceCoreId,
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