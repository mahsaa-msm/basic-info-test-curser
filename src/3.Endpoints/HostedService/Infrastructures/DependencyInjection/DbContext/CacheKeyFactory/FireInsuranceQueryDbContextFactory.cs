using Vehicle.Insurance.Core.Contracts.Common.Services.Tenant;
using Vehicle.Insurance.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;

namespace Vehicle.Insurance.Endpoints.HostedService.Infrastructures.DependencyInjection.DbContext.CacheKeyFactory;

public class VehicleInsuranceQueryDbContextFactory : IVehicleInsuranceQueryDbContextFactory
{
    private readonly IDbContextFactory<VehicleInsuranceQueryDbContext> _internalFactory;
    private readonly ITenantService _tenantService;

    public VehicleInsuranceQueryDbContextFactory(IDbContextFactory<VehicleInsuranceQueryDbContext> internalFactory,
                                             ITenantService tenantService)
    {
        _internalFactory = internalFactory;
        _tenantService = tenantService;
    }
    public VehicleInsuranceQueryDbContext CreateDbContext()
    {
        var context = _internalFactory.CreateDbContext();
        context.TenantId = _tenantService.GetCurrentTenantId();
        context.TenantKey = _tenantService.GetCurrentTenantKey();
        return context;
    }
}
