using Master.Data.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Master.Data.Endpoints.HostedService.Infrastructures.DependencyInjection.DbContext.CacheKeyFactory;

public class TenantModelQueryCacheKeyFactory : IModelCacheKeyFactory
{
    public object Create(Microsoft.EntityFrameworkCore.DbContext context, bool designTime)
    {
        var masterDataQueryDbContext = context as MasterDataQueryDbContext;
        return new TenantModelCacheKey(context.GetType(),
                                       masterDataQueryDbContext?.TenantId,
                                       masterDataQueryDbContext?.TenantKey);
    }
}
