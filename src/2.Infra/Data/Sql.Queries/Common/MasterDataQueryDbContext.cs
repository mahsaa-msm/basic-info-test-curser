using Master.Data.Infra.Data.Sql.Queries.Tenants.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zamin.Infra.Data.Sql.Queries;

namespace Master.Data.Infra.Data.Sql.Queries.Common;

public class MasterDataQueryDbContext : BaseQueryDbContext
{
    public DbSet<Tenant> Tenants { get; set; }

    public MasterDataQueryDbContext(DbContextOptions<MasterDataQueryDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}