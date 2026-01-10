using Master.Data.Infra.Data.Sql.Queries.Common;

namespace Master.Data.Endpoints.HostedService.Infrastructures.DependencyInjection.DbContext.CacheKeyFactory;

public interface IMasterDataQueryDbContextFactory
{
    MasterDataQueryDbContext CreateDbContext();
}