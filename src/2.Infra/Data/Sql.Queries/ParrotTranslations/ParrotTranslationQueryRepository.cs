using Master.Data.Core.Contracts.Common.Options;
using Master.Data.Core.Contracts.ParrotTranslations.Queries;
using Master.Data.Core.RequestResponse.ParrotTranslations.Queries.CommonResults;
using Master.Data.Core.RequestResponse.ParrotTranslations.Queries.GetAll;
using Master.Data.Core.RequestResponse.ParrotTranslations.Queries.GetById;
using Master.Data.Core.RequestResponse.ParrotTranslations.Queries.GetPagedFilter;
using Master.Data.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Infra.Data.Sql.Queries;

namespace Agent.Management.Infra.Data.Sql.Queries.ParrotTranslations;

public sealed class ParrotTranslationQueryRepository : BaseQueryRepository<MasterDataQueryDbContext>, IParrotTranslationQueryRepository
{
    private readonly MasterDataOptions _masterDataOptions;

    public ParrotTranslationQueryRepository(MasterDataQueryDbContext dbContext,
                                            MasterDataOptions masterDataOptions)
        : base(dbContext)
    {
        _masterDataOptions = masterDataOptions;
    }

    public async Task<ParrotTranslationItemQr?> Execute(GetParrotTranslationByIdQuery query)
        => await _dbContext.ParrotTranslations
                .Select(item => new ParrotTranslationItemQr()
                {
                    Id = item.Id,
                    Key = item.Key,
                    Value = item.Value,
                    Culture = item.Culture
                })
                .FirstOrDefaultAsync(app => app.Id == query.Id);

    public async Task<PagedData<ParrotTranslationItemQr>> ExecuteAsync(GetParrotTranslationPagedFilterQuery query)
    {
        var filter = _dbContext.ParrotTranslations.AsQueryable();

        if (!string.IsNullOrEmpty(query.Key))
            filter = filter.Where(item => item.Key.Contains(query.Key));

        if (!string.IsNullOrEmpty(query.Value))
            filter = filter.Where(item => item.Value.Contains(query.Value));

        if (!string.IsNullOrEmpty(query.Culture))
            filter = filter.Where(item => !string.IsNullOrEmpty(item.Culture) && item.Culture.Contains(query.Culture));

        return await filter.ToPagedData(query, item => new ParrotTranslationItemQr
        {
            Id = item.Id,
            Key = item.Key,
            Value = item.Value,
            Culture = item.Culture
        });
    }

    public async Task<ParrotTranslationQr> ExecuteAsync(GetAllParrotTranslationQuery query)
    {
        var item = new ParrotTranslationQr();
        item.ExpireDateUtc = DateTime.UtcNow.AddDays(_masterDataOptions.TranslationsExpireTimeByDay);

        item.ParrotTranslationItemQrs = await _dbContext.ParrotTranslations
            .Select(item => new ParrotTranslationItemQr
            {
                Id = item.Id,
                Key = item.Key,
                Value = item.Value,
                Culture = item.Culture
            }).ToListAsync();

        return item;
    }
}
