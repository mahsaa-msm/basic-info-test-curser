using Master.Data.Core.Contracts.Common.Services;
using Master.Data.Infra.Data.Sql.Queries.Common.Entites;
using Master.Data.Infra.Data.Sql.Queries.Countries.Entities;
using Master.Data.Infra.Data.Sql.Queries.Tenants.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zamin.Infra.Data.Sql.Queries;

namespace Master.Data.Infra.Data.Sql.Queries.Common;

public class MasterDataQueryDbContext : BaseQueryDbContext
{
    private readonly ITenantService _tenantService;

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Country> Countries { get; set; }

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
                var method = typeof(MasterDataQueryDbContext)?
                    .GetMethod(nameof(SetGlobalQueryFilter), BindingFlags.NonPublic | BindingFlags.Instance)?
                    .MakeGenericMethod(entityType.ClrType);

                method?.Invoke(this, new object[] { builder });
            }
        }
    }
    private void SetGlobalQueryFilter<T>(ModelBuilder modelBuilder)
    where T : BaseTenantEntity
    {
        var tenantId = _tenantService.GetCurrentTenantId();

        modelBuilder.Entity<T>().HasQueryFilter(e =>
            EF.Property<long>(e, nameof(BaseTenantEntity.TenantId)) == tenantId);

        var tenantKey = _tenantService.GetCurrentTenantKey();
        if (tenantKey.HasValue)
            modelBuilder.Entity<T>()
                .HasQueryFilter(e =>
                    EF.Property<Guid?>(e, nameof(BaseTenantEntity.TenantBusinessId)) == null ||
                    EF.Property<Guid?>(e, nameof(BaseTenantEntity.TenantBusinessId)) == tenantKey);
    }
}