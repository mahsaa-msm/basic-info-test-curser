using Master.Data.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Master.Data.Endpoints.HostedService.Infrastructures.DependencyInjection.DbContext.CacheKeyFactory;

public class TenantModelCommandCacheKeyFactory : IModelCacheKeyFactory
{
    public object Create(Microsoft.EntityFrameworkCore.DbContext context, bool designTime)
    {
        var masterDataCommandDbContext = context as MasterDataCommandDbContext;
        return new TenantModelCacheKey(context.GetType(),
                                       masterDataCommandDbContext?.TenantId,
                                       masterDataCommandDbContext?.TenantKey);
    }
}
