using Microsoft.EntityFrameworkCore.Infrastructure;
using Vehicle.Insurance.Infra.Data.Sql.Commands.Common;

namespace Vehicle.Insurance.Endpoints.API.Infrastructor.DependencyInjection.DbContext.CacheKeyFactory;

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
