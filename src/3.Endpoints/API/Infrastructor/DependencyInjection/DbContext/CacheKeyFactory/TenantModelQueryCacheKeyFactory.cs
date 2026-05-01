using Microsoft.EntityFrameworkCore.Infrastructure;
using Vehicle.Insurance.Infra.Data.Sql.Queries.Common;

namespace Vehicle.Insurance.Endpoints.API.Infrastructor.DependencyInjection.DbContext.CacheKeyFactory;

public class TenantModelQueryCacheKeyFactory : IModelCacheKeyFactory
{
    public object Create(Microsoft.EntityFrameworkCore.DbContext context, bool designTime)
    {
        var vehicleInsuranceQueryDbContext = context as VehicleInsuranceQueryDbContext;
        return new TenantModelCacheKey(context.GetType(),
                                       vehicleInsuranceQueryDbContext?.TenantId,
                                       vehicleInsuranceQueryDbContext?.TenantKey);
    }
}
