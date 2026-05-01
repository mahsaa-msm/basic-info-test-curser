using Microsoft.EntityFrameworkCore;
using Vehicle.Insurance.Core.Contracts.Common.Services.Tenant;
using Vehicle.Insurance.Infra.Data.Sql.Commands.Common;

namespace Vehicle.Insurance.Endpoints.HostedService.Infrastructures.DependencyInjection.DbContext.CacheKeyFactory;

public class VehicleInsuranceCommandDbContextFactory : IVehicleInsuranceCommandDbContextFactory
{
    private readonly IDbContextFactory<VehicleInsuranceCommandDbContext> _internalFactory;
    private readonly ITenantService _tenantService;

    public VehicleInsuranceCommandDbContextFactory(IDbContextFactory<VehicleInsuranceCommandDbContext> internalFactory,
                                             ITenantService tenantService)
    {
        _internalFactory = internalFactory;
        _tenantService = tenantService;
    }
    public VehicleInsuranceCommandDbContext CreateDbContext()
    {
        var context = _internalFactory.CreateDbContext();
        context.TenantId = _tenantService.GetCurrentTenantId();
        context.TenantKey = _tenantService.GetCurrentTenantKey();
        return context;
    }
}
