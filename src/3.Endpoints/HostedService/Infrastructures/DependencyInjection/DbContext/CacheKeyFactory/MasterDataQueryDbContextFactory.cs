using Master.Data.Core.Contracts.Common.Services.Tenant;
using Master.Data.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;

namespace Master.Data.Endpoints.HostedService.Infrastructures.DependencyInjection.DbContext.CacheKeyFactory;

public class MasterDataQueryDbContextFactory : IMasterDataQueryDbContextFactory
{
    private readonly IDbContextFactory<MasterDataQueryDbContext> _internalFactory;
    private readonly ITenantService _tenantService;

    public MasterDataQueryDbContextFactory(IDbContextFactory<MasterDataQueryDbContext> internalFactory,
                                             ITenantService tenantService)
    {
        _internalFactory = internalFactory;
        _tenantService = tenantService;
    }
    public MasterDataQueryDbContext CreateDbContext()
    {
        var context = _internalFactory.CreateDbContext();
        context.TenantId = _tenantService.GetCurrentTenantId();
        context.TenantKey = _tenantService.GetCurrentTenantKey();
        return context;
    }
}
