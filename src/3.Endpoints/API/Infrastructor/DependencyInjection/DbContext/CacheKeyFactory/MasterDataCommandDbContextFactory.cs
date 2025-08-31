using Master.Data.Core.Contracts.Common.Services;
using Master.Data.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore;

namespace Master.Data.Endpoints.API.Infrastructor.DependencyInjection.DbContext.CacheKeyFactory;

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
