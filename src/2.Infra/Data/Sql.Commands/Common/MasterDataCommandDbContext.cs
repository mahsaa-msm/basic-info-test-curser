using Master.Data.Core.Contracts.Common.Services;
using Master.Data.Core.Domain.Common.Entities;
using Master.Data.Core.Domain.Tenants.Entities;
using Master.Data.Infra.Data.Sql.Commands.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zamin.Extensions.Events.Outbox.Dal.EF;
using Zamin.Infra.Data.Sql.Commands;

namespace Master.Data.Infra.Data.Sql.Commands.Common;

public class MasterDataCommandDbContext : BaseOutboxCommandDbContext
{
    private readonly ITenantService _tenantService;

    public DbSet<Tenant> Tenants { get; set; }

    public MasterDataCommandDbContext(DbContextOptions<MasterDataCommandDbContext> options,
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
                var method = typeof(BaseCommandDbContext)
                    .GetMethod(nameof(SetGlobalQueryFilter), BindingFlags.NonPublic | BindingFlags.Static)
                    .MakeGenericMethod(entityType.ClrType);

                method.Invoke(null, new object[] { builder });
            }
        }
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddConversions();
        base.ConfigureConventions(configurationBuilder);
    }

    private void SetGlobalQueryFilter<T>(ModelBuilder modelBuilder)
        where T : BaseTenantEntity
    {
        modelBuilder.Entity<T>().HasQueryFilter(e =>
            EF.Property<long>(e, nameof(BaseTenantEntity.TenantId)) == _tenantService.GetCurrentTenantId());
    }

}