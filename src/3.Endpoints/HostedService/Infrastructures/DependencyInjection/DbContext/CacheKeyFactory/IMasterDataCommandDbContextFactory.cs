using Master.Data.Infra.Data.Sql.Commands.Common;

namespace Master.Data.Endpoints.HostedService.Infrastructures.DependencyInjection.DbContext.CacheKeyFactory;

public interface IMasterDataCommandDbContextFactory
{
    MasterDataCommandDbContext CreateDbContext();
}