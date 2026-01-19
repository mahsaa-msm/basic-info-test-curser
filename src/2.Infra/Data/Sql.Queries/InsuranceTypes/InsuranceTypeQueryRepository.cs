using Master.Data.Core.Contracts.InsuranceTypes.Queries;
using Master.Data.Core.RequestResponse.InsuranceTypes.Queries.GetAll;
using Master.Data.Core.RequestResponse.InsuranceTypes.Queries.GetAllPagedFilter;
using Master.Data.Core.RequestResponse.InsuranceTypes.Queries.GetById;
using Master.Data.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Infra.Data.Sql.Queries;
using Zamin.Utilities.Extensions;

namespace Master.Data.Infra.Data.Sql.Queries.InsuranceTypes;
public sealed class InsuranceTypeQueryRepository : BaseQueryRepository<MasterDataQueryDbContext>,
    IInsuranceTypeQueryRepository
{
    public InsuranceTypeQueryRepository(MasterDataQueryDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<List<InsuranceTypeSelectItemQr>> Execute(GetAllInsuranceTypeQuery query)
        => await _dbContext.InsuranceTypes
        .Where(c => !query.IsActive.HasValue || c.IsActive == query.IsActive)
        .OrderBy(c => c.Priority)
        .Select(c => new InsuranceTypeSelectItemQr
        {
            CoreId = c.CoreId,
            DisplayTitle = c.DisplayTitle
        }).ToListAsync();

    public async Task<PagedData<InsuranceTypeListItemQr>> Execute(GetAllInsuranceTypesPagedFilterQuery query)
    {
        var result = new PagedData<InsuranceTypeListItemQr>
        {
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };

        var filter = _dbContext.InsuranceTypes.AsQueryable();

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

        if (query.NeedTotalCount is not false)
            result.TotalCount = await filter.CountAsync();

        if (!string.IsNullOrWhiteSpace(query.SortBy))
            filter = filter.OrderByField(query.SortBy, query.SortAscending);

        filter = filter.Skip(query.SkipCount).Take(query.PageSize);

        result.QueryResult = await filter.Select(c => new InsuranceTypeListItemQr
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

    public async Task<InsuranceTypeQr?> Execute(GetInsuranceTypeByIdQuery query)
         => await _dbContext.InsuranceTypes
                .Select(c => new InsuranceTypeQr
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
                .FirstOrDefaultAsync(c => c.Id == query.InsuranceTypeId);
}