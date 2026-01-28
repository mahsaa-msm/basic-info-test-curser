using Master.Data.Core.Contracts.PatternCatalogs.Queries;
using Master.Data.Core.RequestResponse.PatternCatalogs.Queries.CommonResults;
using Master.Data.Core.RequestResponse.PatternCatalogs.Queries.GetAll;
using Master.Data.Core.RequestResponse.PatternCatalogs.Queries.GetAllPagedFilter;
using Master.Data.Core.RequestResponse.PatternCatalogs.Queries.GetById;
using Master.Data.Core.RequestResponse.PatternCatalogs.Queries.GetByKey;
using Master.Data.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using System.Web;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Infra.Data.Sql.Queries;
using Zamin.Utilities.Extensions;

namespace Master.Data.Infra.Data.Sql.Queries.PatternCatalogs;

public sealed class PatternCatalogQueryRepository : BaseQueryRepository<MasterDataQueryDbContext>,
    IPatternCatalogQueryRepository
{
    public PatternCatalogQueryRepository(MasterDataQueryDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<PagedData<PatternCatalogQr>> Execute(GetAllPatternCatalogsPagedFilterQuery query)
    {
        var filter = _dbContext.PatternCatalogs.AsQueryable();

        filter = filter.WhereIf(!string.IsNullOrEmpty(query.Key),
                                c => c.Key.Contains(query.Key!));

        filter = filter.WhereIf(query.IsActive is not null,
                                c => c.IsActive == query.IsActive);

        filter = filter.WhereIf(query.Priority is not null,
                                c => c.Priority == query.Priority);

        return await filter.ToPagedData(query, c => new PatternCatalogQr
        {
            Key = c.Key,
            EncodedPattern = HttpUtility.UrlEncode(c.Pattern),
            Type = c.Type,
            EncodedDescription = HttpUtility.UrlEncode(c.Description),
            CreatedDateUtc = c.CreatedDateUtc,
            LastModifiedDateUtc = c.LastModifiedDateUtc,
            IsActive = c.IsActive,
            Priority = c.Priority,
        });
    }

    public async Task<PatternCatalogQr?> Execute(GetPatternCatalogByIdQuery query)
         => await _dbContext.PatternCatalogs
                .Where(c => c.Id == query.PatternCatalogId)
                .Select(c => new PatternCatalogQr
                {
                    Key = c.Key,
                    EncodedPattern = HttpUtility.UrlEncode(c.Pattern),
                    Type = c.Type,
                    EncodedDescription = HttpUtility.UrlEncode(c.Description),
                    CreatedDateUtc = c.CreatedDateUtc,
                    LastModifiedDateUtc = c.LastModifiedDateUtc,
                    IsActive = c.IsActive,
                    Priority = c.Priority,
                })
                .FirstOrDefaultAsync();

    public async Task<PatternCatalogQr?> Execute(GetPatternCatalogByKeyQuery query)
     => await _dbContext.PatternCatalogs
            .Where(c => c.Key == query.PatternCatalogKey)
            .Select(c => new PatternCatalogQr
            {
                Key = c.Key,
                EncodedPattern = HttpUtility.UrlEncode(c.Pattern),
                Type = c.Type,
                EncodedDescription = HttpUtility.UrlEncode(c.Description),
                CreatedDateUtc = c.CreatedDateUtc,
                LastModifiedDateUtc = c.LastModifiedDateUtc,
                IsActive = c.IsActive,
                Priority = c.Priority,
            })
            .FirstOrDefaultAsync();

    public async Task<List<PatternCatalogQr>> Execute(GetAllPatternCatalogsQuery query)
        => await _dbContext.PatternCatalogs
            .WhereIf(query.IsActive.HasValue, c => c.IsActive == query.IsActive)
            .WhereIf(query.Type.HasValue, c => c.Type == query.Type)
            .Select(c => new PatternCatalogQr
            {
                Key = c.Key,
                EncodedPattern = HttpUtility.UrlEncode(c.Pattern),
                Type = c.Type,
                EncodedDescription = HttpUtility.UrlEncode(c.Description),
                CreatedDateUtc = c.CreatedDateUtc,
                LastModifiedDateUtc = c.LastModifiedDateUtc,
                IsActive = c.IsActive,
                Priority = c.Priority,
            }).ToListAsync();
}