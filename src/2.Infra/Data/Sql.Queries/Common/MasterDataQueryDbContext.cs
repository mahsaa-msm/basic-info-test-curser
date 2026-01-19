using Master.Data.Infra.Data.Sql.Queries.AgreementObligations.Entities;
using Master.Data.Infra.Data.Sql.Queries.Cities.Entities;
using Master.Data.Infra.Data.Sql.Queries.Common.Entites;
using Master.Data.Infra.Data.Sql.Queries.Countries.Entities;
using Master.Data.Infra.Data.Sql.Queries.InsuranceUnits.Entities;
using Master.Data.Infra.Data.Sql.Queries.ParrotTranslations.Entites;
using Master.Data.Infra.Data.Sql.Queries.PatternCatalogs.Entities;
using Master.Data.Infra.Data.Sql.Queries.Provinces.Entities;
using Master.Data.Infra.Data.Sql.Queries.ServiceFeatures.Entities;
using Master.Data.Infra.Data.Sql.Queries.Tenants.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zamin.Infra.Data.Sql.Queries;

namespace Master.Data.Infra.Data.Sql.Queries.Common;

public class MasterDataQueryDbContext : BaseQueryDbContext
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
    #endregion

    public MasterDataQueryDbContext(DbContextOptions<MasterDataQueryDbContext> options)
        : base(options)
    {
    }

    #region Methods
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // اعمال فیلتر برای تمام موجودیت‌های BaseTenantEntity
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(BaseTenantEntity).IsAssignableFrom(entityType.ClrType) &&
        !entityType.IsKeyless &&
        entityType.FindPrimaryKey() != null)
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
    #endregion
}