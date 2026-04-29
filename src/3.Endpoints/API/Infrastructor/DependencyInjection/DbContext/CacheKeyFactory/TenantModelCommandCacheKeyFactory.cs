using Vehicle.Insurance.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Vehicle.Insurance.Endpoints.API.Infrastructor.DependencyInjection.DbContext.CacheKeyFactory;

public class TenantModelCommandCacheKeyFactory : IModelCacheKeyFactory
{
    public object Create(Microsoft.EntityFrameworkCore.DbContext context, bool designTime)
    {
        var masterDataCommandDbContext = context as VehicleInsuranceCommandDbContext;
        return new TenantModelCacheKey(context.GetType(),
                                       masterDataCommandDbContext?.TenantId,
                                       masterDataCommandDbContext?.TenantKey);
    }
}

