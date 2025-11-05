using Master.Data.Endpoints.HostedService.Infrastructures.DependencyInjection.DbContext.CacheKeyFactory;
using Master.Data.Infra.Data.Sql.Commands.Common;
using Master.Data.Infra.Data.Sql.Commands.Common.Interceptors;
using Master.Data.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Zamin.Infra.Data.Sql.Commands.Interceptors;

namespace Master.Data.Endpoints.HostedService.Infrastructures.DependencyInjection.DbContext;

public static class DbContextExtensions
{
    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<SetPersianYeKeInterceptor>();
        services.AddTransient<AddAuditDataInterceptor>();
        services.AddTransient<AddRelatedEntitiesIdInterceptor>();
        //services.AddTransient<TenantQueryCommandDbInterceptor>();
        //services.AddTransient<TenantQueryQueryDbIntrerceptor>();

        //CommandDbContext
        services.AddDbContextFactory<MasterDataCommandDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("CommandDb_ConnectionString"))
                //.LogTo(Console.WriteLine, LogLevel.Information)
                .AddInterceptors(new SetPersianYeKeInterceptor(),
                                 new AddAuditDataInterceptor(),
                                 new AddRelatedEntitiesIdInterceptor());
            options.ReplaceService<IModelCacheKeyFactory, TenantModelCommandCacheKeyFactory>();
        });

        services.AddScoped<IMasterDataCommandDbContextFactory, MasterDataCommandDbContextFactory>();

        services.AddScoped(serviceProvider =>
        {
            var factory = serviceProvider.GetService<IMasterDataCommandDbContextFactory>();
            return factory.CreateDbContext();
        });

        //QueryDbContext
        services.AddDbContextFactory<MasterDataQueryDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("QueryDb_ConnectionString"));
            //.LogTo(Console.WriteLine, LogLevel.Information);
            options.ReplaceService<IModelCacheKeyFactory, TenantModelQueryCacheKeyFactory>();
        });

        services.AddScoped<IMasterDataQueryDbContextFactory, MasterDataQueryDbContextFactory>();

        services.AddScoped(serviceProvider =>
        {
            var factory = serviceProvider.GetService<IMasterDataQueryDbContextFactory>();
            return factory.CreateDbContext();
        });

        return services;
    }
}
