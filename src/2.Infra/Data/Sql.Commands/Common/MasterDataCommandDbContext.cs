using Master.Data.Core.Domain.Tenants.Entities;
using Master.Data.Infra.Data.Sql.Commands.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zamin.Extensions.Events.Outbox.Dal.EF;

namespace Master.Data.Infra.Data.Sql.Commands.Common;

public class MasterDataCommandDbContext : BaseOutboxCommandDbContext
{
    public DbSet<Tenant> Tenants { get; set; }

    public MasterDataCommandDbContext(DbContextOptions<MasterDataCommandDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddConversions();
        base.ConfigureConventions(configurationBuilder);
    }
}