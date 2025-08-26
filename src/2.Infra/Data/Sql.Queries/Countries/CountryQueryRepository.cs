using Master.Data.Core.Contracts.Countries.Queries;
using Master.Data.Core.RequestResponse.Countries.Queries.GetAll;
using Master.Data.Core.RequestResponse.Countries.Queries.GetAllPagedFilter;
using Master.Data.Core.RequestResponse.Countries.Queries.GetById;
using Master.Data.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Infra.Data.Sql.Queries;
using Zamin.Utilities.Extensions;

namespace Master.Data.Infra.Data.Sql.Queries.Countries;
public sealed class CountryQueryRepository : BaseQueryRepository<MasterDataQueryDbContext>,
    ICountryQueryRepository
{
    public CountryQueryRepository(MasterDataQueryDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<List<CountrySelectItemQr>> Execute(GetAllCountryQuery query)
        => await _dbContext.Countries
        .Where(c => !query.IsActive.HasValue || c.IsActive == query.IsActive)
        .OrderBy(c => c.Priority)
        .Select(c => new CountrySelectItemQr
        {
            CoreId = c.CoreId,
            DisplayTitle = c.DisplayTitle
        }).ToListAsync();

    public async Task<PagedData<CountryListItemQr>> Execute(GetAllCountriesPagedFilterQuery query)
    {
        var result = new PagedData<CountryListItemQr>
        {
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };

        var filter = _dbContext.Countries.AsQueryable();

        if (!string.IsNullOrEmpty(query.CoreId))
            filter = filter.Where(c => c.CoreId == query.CoreId);

        if (!string.IsNullOrEmpty(query.Title))
            filter = filter.Where(c => c.Title.Contains(query.Title));

        if (!string.IsNullOrEmpty(query.DisplayTitle))
            filter = filter.Where(c => c.DisplayTitle.Contains(query.DisplayTitle));

        if (!string.IsNullOrEmpty(query.Code))
            filter = filter.Where(c => c.Code.Contains(query.Code));

        if (query.IsActive != null)
            filter = filter.Where(c => c.IsActive == query.IsActive);

        if (query.Priority != null)
            filter = filter.Where(c => c.Priority == query.Priority);

        if (query.NeedTotalCount)
            result.TotalCount = await filter.CountAsync();

        if (!string.IsNullOrWhiteSpace(query.SortBy))
            filter = filter.OrderByField(query.SortBy, query.SortAscending);

        filter = filter.Skip(query.SkipCount).Take(query.PageSize);

        result.QueryResult = await filter.Select(c => new CountryListItemQr
        {
            Id = c.Id,
            CoreId = c.CoreId,
            Title = c.Title,
            DisplayTitle = c.DisplayTitle,
            Code = c.Code,
            Priority = c.Priority,
            IsActive = c.IsActive,
        }).ToListAsync();

        return result;
    }

    public async Task<CountryQr?> Execute(GetCountryByIdQuery query)
         => await _dbContext.Countries
                .Select(c => new CountryQr
                {
                    Id = c.Id,
                    CoreId = c.CoreId,
                    Title = c.Title,
                    DisplayTitle = c.DisplayTitle,
                    Code = c.Code,
                    Priority = c.Priority,
                    IsActive = c.IsActive,
                    IsEditable = !string.IsNullOrEmpty(c.CreatedByUserId)

                })
                .FirstOrDefaultAsync(c => c.Id == query.CountryId);
}