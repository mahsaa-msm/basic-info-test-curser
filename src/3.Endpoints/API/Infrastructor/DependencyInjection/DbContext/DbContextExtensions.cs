using Master.Data.Core.ApplicationService.Tenants;
using Master.Data.Core.Contracts.Common.Services.Tenant;
using Master.Data.Core.RequestResponse.Tenants.Queries.GetById;
using Master.Data.Endpoints.API.Infrastructor.DependencyInjection.DbContext.CacheKeyFactory;
using Master.Data.Infra.Data.Sql.Commands.Common;
using Master.Data.Infra.Data.Sql.Commands.Common.Interceptors;
using Master.Data.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Zamin.Infra.Data.Sql.Commands.Interceptors;

namespace Master.Data.Endpoints.API.Infrastructor.DependencyInjection.DbContext;

public static class DbContextExtensions
{
    private static ITenantService GetTenantService(IServiceScopeFactory serviceScopeFactory)
    {
        var scope = serviceScopeFactory.CreateScope();
        return scope.ServiceProvider.GetRequiredService<ITenantService>();
    }

    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {

        //CommandDbContext
        services.AddDbContextFactory<MasterDataCommandDbContext>((serviceProvider, options) =>
        {

            var setPersianYeKeInterceptor = serviceProvider.GetRequiredService<SetPersianYeKeInterceptor>();
            var auditInterceptor = serviceProvider.GetRequiredService<AddAuditDataInterceptor>();
            var relatedEntitiesInterceptor = serviceProvider.GetRequiredService<AddRelatedEntitiesIdInterceptor>();

            options.UseSqlServer(configuration.GetConnectionString("CommandDb_ConnectionString"))
                   .AddInterceptors(setPersianYeKeInterceptor, auditInterceptor, relatedEntitiesInterceptor);

            options.ReplaceService<IModelCacheKeyFactory, TenantModelCommandCacheKeyFactory>();
        });

        services.AddScoped<IMasterDataCommandDbContextFactory, MasterDataCommandDbContextFactory>();

        services.AddScoped<MasterDataCommandDbContext>(serviceProvider =>
        {
            var factory = serviceProvider.GetService<IMasterDataCommandDbContextFactory>();
            return factory.CreateDbContext();
        });

        //QueryDbContext
        services.AddDbContextFactory<MasterDataQueryDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("QueryDb_ConnectionString"));
            options.ReplaceService<IModelCacheKeyFactory, TenantModelQueryCacheKeyFactory>();
        });

        services.AddScoped<IMasterDataQueryDbContextFactory, MasterDataQueryDbContextFactory>();

        services.AddScoped<MasterDataQueryDbContext>(serviceProvider =>
        {
            var factory = serviceProvider.GetService<IMasterDataQueryDbContextFactory>();
            return factory.CreateDbContext();
        });

        return services;
    }


}
