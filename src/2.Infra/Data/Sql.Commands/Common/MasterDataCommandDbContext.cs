using Vehicle.Insurance.Core.Domain.AgreementObligations.Entities;
using Vehicle.Insurance.Core.Domain.Cities.Entities;
using Vehicle.Insurance.Core.Domain.Common.Entities;
using Vehicle.Insurance.Core.Domain.Countries.Entities;
using Vehicle.Insurance.Core.Domain.InsuranceTypes.Entities;
using Vehicle.Insurance.Core.Domain.InsuranceUnits.Entities;
using Vehicle.Insurance.Core.Domain.IssuanceSchemes.Entities;
using Vehicle.Insurance.Core.Domain.VehicleColors.Entities;
using Vehicle.Insurance.Core.Domain.ParrotTranslations.Entities;
using Vehicle.Insurance.Core.Domain.PatternCatalogs.Entities;
using Vehicle.Insurance.Core.Domain.Provinces.Entities;
using Vehicle.Insurance.Core.Domain.ServiceFeatures.Entities;
using Vehicle.Insurance.Core.Domain.Tenants.Entities;
using Vehicle.Insurance.Infra.Data.Sql.Commands.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zamin.Extensions.Events.Outbox.Dal.EF;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.Common;

public class VehicleInsuranceCommandDbContext : BaseOutboxCommandDbContext
{
    #region Properties
    public long? TenantId { get; set; }
    public Guid? TenantKey { get; set; }
    #endregion

    #region Entities
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<ParrotTranslation> ParrotTranslations { get; set; } = null!;
    public DbSet<Province> Provinces { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<InsuranceUnit> InsuranceUnits { get; set; } = null!;
    public DbSet<PatternCatalog> PatternCatalogs { get; set; } = null!;
    public DbSet<ServiceFeature> ServiceFeatures { get; set; } = null!;
    public DbSet<InsuranceType> InsuranceTypes { get; set; } = null!;
    public DbSet<VehicleColor> VehicleColors { get; set; } = null!;
    public DbSet<IssuanceScheme> IssuanceSchemes { get; set; } = null!;
    public DbSet<AgreementObligation> AgreementObligations { get; set; } = null!;
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
