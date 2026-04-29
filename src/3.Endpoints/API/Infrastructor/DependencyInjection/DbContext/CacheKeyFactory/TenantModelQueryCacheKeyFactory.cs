using Vehicle.Insurance.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Vehicle.Insurance.Endpoints.API.Infrastructor.DependencyInjection.DbContext.CacheKeyFactory;

public class TenantModelQueryCacheKeyFactory : IModelCacheKeyFactory
{
    public object Create(Microsoft.EntityFrameworkCore.DbContext context, bool designTime)
    {
        var masterDataQueryDbContext = context as VehicleInsuranceQueryDbContext;
        return new TenantModelCacheKey(context.GetType(),
                                       masterDataQueryDbContext?.TenantId,
                                       masterDataQueryDbContext?.TenantKey);
    }
}

