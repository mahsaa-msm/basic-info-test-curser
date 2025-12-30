using Master.Data.Infra.Data.Sql.Commands.Common;

namespace Master.Data.Endpoints.API.Infrastructor.DependencyInjection.DbContext.CacheKeyFactory;

public interface IMasterDataCommandDbContextFactory
{
    MasterDataCommandDbContext CreateDbContext();
}