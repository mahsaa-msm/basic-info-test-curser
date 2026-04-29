using Vehicle.Insurance.Infra.Data.Sql.Queries.AgreementObligations.Entities;
using Vehicle.Insurance.Infra.Data.Sql.Queries.Cities.Entities;
using Vehicle.Insurance.Infra.Data.Sql.Queries.Common.Entites;
using Vehicle.Insurance.Infra.Data.Sql.Queries.Countries.Entities;
using Vehicle.Insurance.Infra.Data.Sql.Queries.InsuranceTypes.Entities;
using Vehicle.Insurance.Infra.Data.Sql.Queries.InsuranceUnits.Entities;
using Vehicle.Insurance.Infra.Data.Sql.Queries.IssuanceSchemes.Entities;
using Vehicle.Insurance.Infra.Data.Sql.Queries.VehicleColors.Entities;
using Vehicle.Insurance.Infra.Data.Sql.Queries.ParrotTranslations.Entites;
using Vehicle.Insurance.Infra.Data.Sql.Queries.PatternCatalogs.Entities;
using Vehicle.Insurance.Infra.Data.Sql.Queries.Provinces.Entities;
using Vehicle.Insurance.Infra.Data.Sql.Queries.ServiceFeatures.Entities;
using Vehicle.Insurance.Infra.Data.Sql.Queries.Tenants.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zamin.Infra.Data.Sql.Queries;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.Common;

public class VehicleInsuranceQueryDbContext : BaseQueryDbContext
{
    #region Properties
    public long? TenantId { get; set; }
    public Guid? TenantKey { get; set; }
    #endregion


    #region Entities
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<ParrotTranslation> ParrotTranslations { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<InsuranceUnit> InsuranceUnits { get; set; }
    public DbSet<PatternCatalog> PatternCatalogs { get; set; }
    public DbSet<ServiceFeature> ServiceFeatures { get; set; }
    public DbSet<AgreementObligation> AgreementObligations { get; set; }
    public DbSet<InsuranceType> InsuranceTypes { get; set; }
    public DbSet<VehicleColor> VehicleColors { get; set; } = null!;
    public DbSet<IssuanceScheme> IssuanceSchemes { get; set; } = null!;

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
