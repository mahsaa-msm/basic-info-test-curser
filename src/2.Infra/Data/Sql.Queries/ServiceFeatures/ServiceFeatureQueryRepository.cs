using Master.Data.Core.Contracts.ServiceFeatures.Queries;
using Master.Data.Core.RequestResponse.ServiceFeatures.Queries.CommonResults;
using Master.Data.Core.RequestResponse.ServiceFeatures.Queries.GetAll;
using Master.Data.Core.RequestResponse.ServiceFeatures.Queries.GetAllPagedFilter;
using Master.Data.Core.RequestResponse.ServiceFeatures.Queries.GetById;
using Master.Data.Core.Resources;
using Master.Data.Core.Resources.Utils.Extensions;
using Master.Data.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Infra.Data.Sql.Queries;
using Zamin.Utilities.Extensions;

namespace Master.Data.Infra.Data.Sql.Queries.ServiceFeatures;

public sealed class ServiceFeatureQueryRepository : BaseQueryRepository<MasterDataQueryDbContext>,
    IServiceFeatureQueryRepository
{
    public ServiceFeatureQueryRepository(MasterDataQueryDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<ServiceFeatureQr?> Execute(GetServiceFeatureByIdQuery query)
         => await _dbContext.ServiceFeatures
                .Select(c => new ServiceFeatureQr
                {
                    Id = c.Id,
                    Key = c.Key,
                    ServiceName = c.ServiceName,
                    FeatureName = c.FeatureName,
                    Description = c.Description,
                    IsActive = c.IsActive,

                })
                .FirstOrDefaultAsync(c => c.Id == query.ServiceFeatureId);

    public async Task<List<ServiceFeatureQr>> Execute(GetAllServiceFeaturesQuery query)
    {
        var result = await _dbContext.ServiceFeatures
                    .WhereIf(query.IsActive.HasValue, c => c.IsActive == query.IsActive)
                    .Select(c => new ServiceFeatureQr
                    {
                        Id = c.Id,
                        Key = c.Key,
                        ServiceName = c.ServiceName,
                        FeatureName = c.FeatureName,
                        Description = c.Description,
                        IsActive = c.IsActive,
                    }).ToListAsync();

        return result.WhereIf(query.Level.HasValue, c => c.Key.GetLevel() == query.Level)
                     .ToList();
    }

    public async Task<PagedData<ServiceFeatureQr>> Execute(GetAllServiceFeaturesPagedFilterQuery query)
    {
        var filter = _dbContext.ServiceFeatures.AsQueryable();

        filter.WhereIf(!string.IsNullOrEmpty(query.ServiceName),
                       c => c.ServiceName == query.ServiceName);

        filter.WhereIf(!string.IsNullOrEmpty(query.FeatureName),
                       c => c.FeatureName == query.FeatureName);

        filter.WhereIf(!string.IsNullOrEmpty(query.Description),
                       c => c.Description == query.Description);

        filter.WhereIf(query.Key is not null,
                       c => c.Key == query.Key);

        filter.WhereIf(query.IsActive is not null,
                       c => c.IsActive == query.IsActive);

        return await filter.ToPagedData(query, c => new ServiceFeatureQr
        {
            Id = c.Id,
            Key = c.Key,
            ServiceName = c.ServiceName,
            FeatureName = c.FeatureName,
            Description = c.Description,
            IsActive = c.IsActive,
        });
    }
}
