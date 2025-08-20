using Master.Data.Infra.Data.Sql.Queries.Common;

namespace Master.Data.Endpoints.API.Infrastructor.DependencyInjection.DbContext.CacheKeyFactory;

public interface IMasterDataQueryDbContextFactory
{
    MasterDataQueryDbContext CreateDbContext();
}