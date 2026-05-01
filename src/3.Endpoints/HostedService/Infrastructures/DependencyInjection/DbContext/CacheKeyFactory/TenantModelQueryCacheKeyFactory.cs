using Vehicle.Insurance.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Vehicle.Insurance.Endpoints.HostedService.Infrastructures.DependencyInjection.DbContext.CacheKeyFactory;

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
