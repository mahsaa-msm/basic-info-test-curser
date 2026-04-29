using Vehicle.Insurance.Core.Contracts.Provinces.Queries;
using Vehicle.Insurance.Core.RequestResponse.Provinces.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.Provinces.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.Provinces.Queries.GetById;
using Vehicle.Insurance.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Infra.Data.Sql.Queries;
using Zamin.Utilities.Extensions;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.Provinces;

public sealed class ProvinceQueryRepository : BaseQueryRepository<VehicleInsuranceQueryDbContext>,
    IProvinceQueryRepository
{
    public ProvinceQueryRepository(VehicleInsuranceQueryDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<List<ProvinceSelectItemQr>> Execute(GetAllProvincesQuery query)
        => await _dbContext.Provinces
        .WhereIf(query.IsActive.HasValue, c => c.IsActive == query.IsActive)
        .WhereIf(query.CountryCoreId.HasValue, c => c.CountryCoreId == query.CountryCoreId.ToString())
        .OrderBy(c => c.Priority)
        .Select(c => new ProvinceSelectItemQr
        {
            CoreId = c.CoreId,
            DisplayTitle = c.DisplayTitle,
            CountryCoreId = c.CountryCoreId,
        }).ToListAsync();

    public async Task<PagedData<ProvinceListItemQr>> Execute(GetAllProvincesPagedFilterQuery query)
    {
        var result = new PagedData<ProvinceListItemQr>
        {
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };

        var filter = _dbContext.Provinces.AsQueryable();

        if (!string.IsNullOrEmpty(query.CoreId))
            filter = filter.Where(c => c.CoreId == query.CoreId);

        if (!string.IsNullOrEmpty(query.Title))
            filter = filter.Where(c => c.Title.Contains(query.Title));

        if (!string.IsNullOrEmpty(query.DisplayTitle))
            filter = filter.Where(c => c.DisplayTitle.Contains(query.DisplayTitle));

        if (!string.IsNullOrEmpty(query.Code))
            filter = filter.Where(c => c.Code.Contains(query.Code));

        if (!string.IsNullOrEmpty(query.CountryCoreId))
            filter = filter.Where(c => c.CountryCoreId == query.CountryCoreId);

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
            .Include(c => c.Country)
            .Select(c => new ProvinceListItemQr
            {
                Id = c.Id,
                CoreId = c.CoreId,
                Title = c.Title,
                DisplayTitle = c.DisplayTitle,
                Code = c.Code,
                CountryCoreId = c.CountryCoreId,
                CountryDisplayTitle = c.Country != null ?
                    c.Country.DisplayTitle :
                    null,
                Priority = c.Priority,
                IsActive = c.IsActive,
            }).ToListAsync();

        return result;
    }

    public async Task<ProvinceQr?> Execute(GetProvinceByIdQuery query)
         => await _dbContext.Provinces
                .Select(c => new ProvinceQr
                {
                    Id = c.Id,
                    CoreId = c.CoreId,
                    Title = c.Title,
                    DisplayTitle = c.DisplayTitle,
                    Code = c.Code,
                    CountryCoreId = c.CountryCoreId,
                    Priority = c.Priority,
                    IsActive = c.IsActive,
                    IsEditable = !string.IsNullOrEmpty(c.CreatedByUserId)

                })
                .FirstOrDefaultAsync(c => c.Id == query.ProvinceId);
}
