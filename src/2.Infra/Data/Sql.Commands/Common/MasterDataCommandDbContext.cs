using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Vehicle.Insurance.Core.Domain.Common.Entities;
using Vehicle.Insurance.Core.Domain.LicensePlateTypes.Entities;
using Vehicle.Insurance.Core.Domain.VehicleBrands.Entities;
using Vehicle.Insurance.Core.Domain.VehicleColors.Entities;
using Vehicle.Insurance.Core.Domain.VehicleTips.Entities;
using Vehicle.Insurance.Infra.Data.Sql.Commands.Common.Extensions;
using Zamin.Extensions.Events.Outbox.Dal.EF;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.Common;

public class VehicleInsuranceCommandDbContext : BaseOutboxCommandDbContext
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

    public VehicleInsuranceCommandDbContext(DbContextOptions<VehicleInsuranceCommandDbContext> options)
        : base(options)
    {
    }

    #region Methods
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
            if (typeof(BaseTenantEntity).IsAssignableFrom(entityType.ClrType) &&
        !entityType.IsKeyless &&
        entityType.FindPrimaryKey() != null)
            {
                var method = typeof(VehicleInsuranceCommandDbContext)?
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
