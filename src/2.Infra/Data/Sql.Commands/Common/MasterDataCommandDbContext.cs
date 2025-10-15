using Master.Data.Core.Contracts.TravelDurationTypess.Queries.CommonResult;
using Master.Data.Core.Domain.Common.Entities;
using Master.Data.Core.Domain.Countries.Entities;
using Master.Data.Core.Domain.Tenants.Entities;
using Master.Data.Core.Domain.TravelDurationTypes.Entities;
using Master.Data.Core.Domain.TravelPassengerCountTypes.Entities;
using Master.Data.Infra.Data.Sql.Commands.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zamin.Extensions.Events.Outbox.Dal.EF;
using Zamin.Infra.Data.Sql.Commands;

namespace Master.Data.Infra.Data.Sql.Commands.Common;

public class MasterDataCommandDbContext : BaseOutboxCommandDbContext
{
    public long? TenantId { get; set; }
    public Guid? TenantKey { get; set; }


    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<TravelDurationType> TravelDurationTypes { get; set; } = null!;
    public DbSet<TravelPassengerCountType> TravelPassengerCountTypes { get; set; } = null!;

    public MasterDataCommandDbContext(DbContextOptions<MasterDataCommandDbContext> options)
        : base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddConversions();
        base.ConfigureConventions(configurationBuilder);
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
                var method = typeof(BaseCommandDbContext)?
                    .GetMethod(nameof(SetGlobalQueryFilter), BindingFlags.NonPublic | BindingFlags.Static)?
                    .MakeGenericMethod(entityType.ClrType);

                method?.Invoke(this, new object[] { builder });
            }
        }
    }

    private void SetGlobalQueryFilter<T>(ModelBuilder modelBuilder)
        where T : BaseTenantEntity
    {
        modelBuilder.Entity<T>().HasQueryFilter(e =>
            TenantId.HasValue ?
                EF.Property<long>(e, nameof(BaseTenantEntity.TenantId)) == TenantId.Value :
                false);

        if (TenantKey.HasValue)
            modelBuilder.Entity<T>()
                .HasQueryFilter(e =>
                    EF.Property<Guid?>(e, nameof(BaseTenantEntity.TenantBusinessId)) == null ||
                    EF.Property<Guid?>(e, nameof(BaseTenantEntity.TenantBusinessId)) == TenantKey);
    }
}