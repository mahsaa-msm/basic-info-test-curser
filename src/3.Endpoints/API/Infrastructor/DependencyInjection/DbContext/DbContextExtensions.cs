using Master.Data.Infra.Data.Sql.Commands.Common;
using Master.Data.Infra.Data.Sql.Commands.Common.Interceptors;
using Master.Data.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Commands.Interceptors;

namespace Master.Data.Endpoints.API.Infrastructor.DependencyInjection.DbContext;

public static class DbContextExtensions
{
    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        //CommandDbContext
        services.AddDbContext<MasterDataCommandDbContext>(c => c.UseSqlServer(configuration.GetConnectionString("CommandDb_ConnectionString"))
            .AddInterceptors(new SetPersianYeKeInterceptor(),
                             new AddAuditDataInterceptor(),
                             new AddRelatedEntitiesIdInterceptor()));

        //QueryDbContext
        services.AddDbContext<MasterDataQueryDbContext>(c => c.UseSqlServer(configuration.GetConnectionString("QueryDb_ConnectionString")));

        return services;
    }

}
