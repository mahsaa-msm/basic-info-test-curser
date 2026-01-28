using Master.Data.Core.Contracts.IssuanceSchemes.Queries;
using Master.Data.Core.RequestResponse.IssuanceSchemes.Queries.GetAll;
using Master.Data.Core.RequestResponse.IssuanceSchemes.Queries.GetAllPagedFilter;
using Master.Data.Core.RequestResponse.IssuanceSchemes.Queries.GetById;
using Master.Data.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Infra.Data.Sql.Queries;
using Zamin.Utilities.Extensions;

namespace Master.Data.Infra.Data.Sql.Queries.IssuanceSchemes;
public sealed class IssuanceSchemeQueryRepository : BaseQueryRepository<MasterDataQueryDbContext>,
    IIssuanceSchemeQueryRepository
{
    public IssuanceSchemeQueryRepository(MasterDataQueryDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IssuanceSchemeQr?> Execute(GetIssuanceSchemeByIdQuery query)
         => await _dbContext.IssuanceSchemes
                .Select(c => new IssuanceSchemeQr
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
                .FirstOrDefaultAsync(c => c.Id == query.IssuanceSchemeId);

    public async Task<List<IssuanceSchemeSelectItemQr>> Execute(GetAllIssuanceSchemeQuery query)
        => await _dbContext.IssuanceSchemes
        .Where(c => !query.IsActive.HasValue || c.IsActive == query.IsActive)
        .OrderBy(c => c.Priority)
        .Select(c => new IssuanceSchemeSelectItemQr
        {
            CoreId = c.CoreId,
            DisplayTitle = c.DisplayTitle,
        }).ToListAsync();

    public async Task<PagedData<IssuanceSchemeListItemQr>> Execute(GetAllIssuanceSchemesPagedFilterQuery query)
    {
        var filter = _dbContext.IssuanceSchemes.AsQueryable();

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

        filter = filter.WhereIf(query.Priority is not null,
                                c => c.Priority == query.Priority);

        return await filter.ToPagedData(query, c => new IssuanceSchemeListItemQr
        {
            Id = c.Id,
            Title = c.Title,
            DisplayTitle = c.DisplayTitle,
            Code = c.Code,
            CoreId = c.CoreId,
            IsActive = c.IsActive,
            Priority = c.Priority,
        });
    }
}
