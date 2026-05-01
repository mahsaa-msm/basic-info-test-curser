using Vehicle.Insurance.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Vehicle.Insurance.Endpoints.HostedService.Infrastructures.DependencyInjection.DbContext.CacheKeyFactory;

public class TenantModelCommandCacheKeyFactory : IModelCacheKeyFactory
{
    public object Create(Microsoft.EntityFrameworkCore.DbContext context, bool designTime)
    {
        var vehicleInsuranceCommandDbContext = context as VehicleInsuranceCommandDbContext;
        return new TenantModelCacheKey(context.GetType(),
                                       vehicleInsuranceCommandDbContext?.TenantId,
                                       vehicleInsuranceCommandDbContext?.TenantKey);
    }
}
