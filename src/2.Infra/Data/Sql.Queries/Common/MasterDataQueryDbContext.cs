using Master.Data.Infra.Data.Sql.Queries.Common.Entites;
using Master.Data.Infra.Data.Sql.Queries.Countries.Entities;
using Master.Data.Infra.Data.Sql.Queries.Tenants.Entities;
using Master.Data.Infra.Data.Sql.Queries.TravelPassengerCountTypes.Entites;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zamin.Infra.Data.Sql.Queries;

namespace Master.Data.Infra.Data.Sql.Queries.Common;

public class MasterDataQueryDbContext : BaseQueryDbContext
{
    public long? TenantId { get; set; }
    public Guid? TenantKey { get; set; }


    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<TravelPassengerCountType> TravelPassengerCountTypes { get; set; }

    public MasterDataQueryDbContext(DbContextOptions<MasterDataQueryDbContext> options)
        : base(options)
    {
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