using Master.Data.Core.Contracts.Common.Services.Tenant;
using Master.Data.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore;

namespace Master.Data.Endpoints.HostedService.Infrastructures.DependencyInjection.DbContext.CacheKeyFactory;

public class MasterDataCommandDbContextFactory : IMasterDataCommandDbContextFactory
{
    private readonly IDbContextFactory<MasterDataCommandDbContext> _internalFactory;
    private readonly ITenantService _tenantService;

    public MasterDataCommandDbContextFactory(IDbContextFactory<MasterDataCommandDbContext> internalFactory,
                                             ITenantService tenantService)
    {
        _internalFactory = internalFactory;
        _tenantService = tenantService;
    }
    public MasterDataCommandDbContext CreateDbContext()
    {
        var context = _internalFactory.CreateDbContext();
        context.TenantId = _tenantService.GetCurrentTenantId();
        context.TenantKey = _tenantService.GetCurrentTenantKey();
        return context;
    }
}
