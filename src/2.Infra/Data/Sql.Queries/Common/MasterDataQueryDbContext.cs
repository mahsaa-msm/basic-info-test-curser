using Master.Data.Core.Contracts.Common.Services;
using Master.Data.Core.Domain.Common.Entities;
using Master.Data.Infra.Data.Sql.Queries.Tenants.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zamin.Infra.Data.Sql.Queries;

namespace Master.Data.Infra.Data.Sql.Queries.Common;

public class MasterDataQueryDbContext : BaseQueryDbContext
{
    private readonly ITenantService _tenantService;

    public DbSet<Tenant> Tenants { get; set; }

    public MasterDataQueryDbContext(DbContextOptions<MasterDataQueryDbContext> options,
                                    ITenantService tenantService)
        : base(options)
    {
        _tenantService = tenantService;
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);

        // اعمال فیلتر برای تمام موجودیت‌های BaseTenantEntity
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(BaseTenantEntity).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(BaseQueryDbContext)
                    .GetMethod(nameof(SetGlobalQueryFilter), BindingFlags.NonPublic | BindingFlags.Static)
                    .MakeGenericMethod(entityType.ClrType);

                method.Invoke(null, new object[] { builder });
            }
        }
    }
    private void SetGlobalQueryFilter<T>(ModelBuilder modelBuilder)
    where T : BaseTenantEntity
    {
        modelBuilder.Entity<T>().HasQueryFilter(e =>
            EF.Property<long>(e, nameof(BaseTenantEntity.TenantId)) == _tenantService.GetCurrentTenantId());
    }
}