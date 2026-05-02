using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Vehicle.Insurance.Infra.Data.Sql.Queries.Common.Entites;
using Vehicle.Insurance.Infra.Data.Sql.Queries.LicensePlateTypes.Entities;
using Vehicle.Insurance.Infra.Data.Sql.Queries.VehicleBrands.Entities;
using Vehicle.Insurance.Infra.Data.Sql.Queries.VehicleColors.Entities;
using Vehicle.Insurance.Infra.Data.Sql.Queries.VehicleTips.Entities;
using Zamin.Infra.Data.Sql.Queries;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.Common;

public class VehicleInsuranceQueryDbContext : BaseQueryDbContext
{
    #region Properties
    public long? TenantId { get; set; }
    public Guid? TenantKey { get; set; }
    #endregion


    #region Entities
    public DbSet<VehicleColor> VehicleColors { get; set; } = null!;
    public DbSet<LicensePlateType> LicensePlateTypes { get; set; } = null!;
    public DbSet<VehicleBrand> VehicleBrands { get; set; } = null!;
    public DbSet<VehicleTip> VehicleTips { get; set; } = null!;

    #endregion

    public VehicleInsuranceQueryDbContext(DbContextOptions<VehicleInsuranceQueryDbContext> options)
        : base(options)
    {
    }

    #region Methods

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);

        // اعمال فیلتر برای تمام موجودیت‌های BaseTenantEntity
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(BaseTenantEntity).IsAssignableFrom(entityType.ClrType) &&
                !entityType.IsKeyless &&
                entityType.FindPrimaryKey() != null)
            {
                var method = typeof(VehicleInsuranceQueryDbContext)?
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
    #endregion
}
