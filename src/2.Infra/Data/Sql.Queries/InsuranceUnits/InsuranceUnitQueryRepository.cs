using Master.Data.Core.Contracts.InsuranceUnits.Queries;
using Master.Data.Core.RequestResponse.InsuranceUnits.Queries.GetAllInArea;
using Master.Data.Core.RequestResponse.InsuranceUnits.Queries.GetAllPagedFilter;
using Master.Data.Core.RequestResponse.InsuranceUnits.Queries.GetById;
using Master.Data.Infra.Data.Sql.Queries.Common;
using Master.Data.Infra.Data.Sql.Queries.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Infra.Data.Sql.Queries;
using Zamin.Utilities.Extensions;

namespace Master.Data.Infra.Data.Sql.Queries.InsuranceUnits;
public sealed class InsuranceUnitQueryRepository : BaseQueryRepository<MasterDataQueryDbContext>,
    IInsuranceUnitQueryRepository
{
    public InsuranceUnitQueryRepository(MasterDataQueryDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<PagedData<InsuranceUnitListItemQr>> Execute(GetAllInsuranceUnitsPagedFilterQuery query)
    {
        var result = new PagedData<InsuranceUnitListItemQr>
        {
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };

        var filter = _dbContext.InsuranceUnits.AsQueryable();

        if (!string.IsNullOrEmpty(query.CoreId))
            filter = filter.Where(c => c.CoreId == query.CoreId);

        if (!string.IsNullOrEmpty(query.Name))
            filter = filter.Where(c => c.Name.Contains(query.Name));

        if (!string.IsNullOrEmpty(query.Title))
            filter = filter.Where(c => c.Title.Contains(query.Title));

        if (!string.IsNullOrEmpty(query.DisplayTitle))
            filter = filter.Where(c => c.DisplayTitle.Contains(query.DisplayTitle));

        if (!string.IsNullOrEmpty(query.Code))
            filter = filter.Where(c => c.Code.Contains(query.Code));

        if (!string.IsNullOrEmpty(query.CityCoreId))
            filter = filter.Where(c => c.CityCoreId == query.CityCoreId);

        if (!string.IsNullOrEmpty(query.ProvinceCoreId))
            filter = filter.Include(c => c.City).Where(c => c.City.ProvinceCoreId == query.ProvinceCoreId);

        if (query.Latitude.HasValue)
            filter = filter.Where(c => c.Location != null && c.Location.Y == query.Latitude.Value);

        if (query.Longitude.HasValue)
            filter = filter.Where(c => c.Location != null && c.Location.X == query.Longitude.Value);

        if (query.IsActive != null)
            filter = filter.Where(c => c.IsActive == query.IsActive);

        if (query.Priority != null)
            filter = filter.Where(c => c.Priority == query.Priority);

        if (query.NeedTotalCount is not false)
            result.TotalCount = await filter.CountAsync();

        if (!string.IsNullOrWhiteSpace(query.SortBy))
            filter = filter.OrderByField(query.SortBy, query.SortAscending);

        filter = filter.Skip(query.SkipCount).Take(query.PageSize);

        result.QueryResult = await filter.Select(c => new InsuranceUnitListItemQr
        {
            Id = c.Id,
            CoreId = c.CoreId,
            Name = c.Name,
            Title = c.Title,
            DisplayTitle = c.DisplayTitle,
            Code = c.Code,
            CityCoreId = c.CityCoreId,
            //Latitude = c.Location.Y,
            Latitude = c.Location != null ? c.Location.Y : (double?)null,
            //Longitude = c.Location.X,
            Longitude = c.Location != null ? c.Location.X : (double?)null,
            State = c.State,
            Type = c.Type,
            Priority = c.Priority,
            IsActive = c.IsActive,
        }).ToListAsync();

        return result;
    }

    public async Task<InsuranceUnitQr?> Execute(GetInsuranceUnitByIdQuery query)
         => await _dbContext.InsuranceUnits
                .Where(c => c.Id == query.InsuranceUnitId)
                .Include(c => c.City)
                .Select(c => new InsuranceUnitQr
                {
                    Id = c.Id,
                    CoreId = c.CoreId,
                    Name = c.Name,
                    Title = c.Title,
                    DisplayTitle = c.DisplayTitle,
                    Code = c.Code,
                    State = c.State,
                    Type = c.Type,
                    //Latitude = c.Location.Y,
                    Latitude = c.Location != null ? c.Location.Y : (double?)null,
                    //Longitude = c.Location.X,
                    Longitude = c.Location != null ? c.Location.X : (double?)null,
                    CityTitle = c.City.DisplayTitle,
                    CityCoreId = c.CityCoreId,
                    Priority = c.Priority,
                    IsActive = c.IsActive,
                    IsEditable = !string.IsNullOrEmpty(c.CreatedByUserId)

                })
                .FirstOrDefaultAsync();

    public async Task<List<InsuranceUnitMapItemQr>> Execute(GetAllInsuranceUnitsInAreaQuery query)
    {
        var filter = _dbContext.InsuranceUnits
            .Include(u => u.City)
            .AsQueryable();

        if (query.Area is not null && query.Area.IsValid())
        {
            var boundingBox = SpatialExtensions.CreateBoundingBox(query.Area.MinLongitude,
                                                                  query.Area.MinLatitude,
                                                                  query.Area.MaxLongitude,
                                                                  query.Area.MaxLatitude);

            filter = filter.Where(u => u.Location != null && u.Location.Within(boundingBox));
        }

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.ProvinceCoreId),
                                u => u.City.ProvinceCoreId == query.ProvinceCoreId);

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.CityCoreId),
                                u => u.CityCoreId == query.CityCoreId);

        filter = filter.WhereIf(query.Type.HasValue,
                                u => u.Type == query.Type!.Value);

        if (!string.IsNullOrEmpty(query.SearchInput))
        {
            var searchTerm = query.SearchInput.Trim();
            filter = filter.Where(u =>
                u.Title.Contains(searchTerm) ||
                u.Code.Contains(searchTerm) ||
                u.Name.Contains(searchTerm));
        }

        return await filter.OrderBy(u => u.Priority)
            .ThenBy(u => u.Title)
            .Take(query.MaxResults)
            .Select(u => new InsuranceUnitMapItemQr
            {
                Id = u.Id,
                Name = u.Name,
                Title = u.Title,
                DisplayTitle = u.DisplayTitle,
                Latitude = u.Location!.Y,
                Longitude = u.Location!.X,
                Type = u.Type,
                CityTitle = u.City.DisplayTitle,
                CityCoreId = u.CityCoreId,
            })
            .ToListAsync();
    }
}