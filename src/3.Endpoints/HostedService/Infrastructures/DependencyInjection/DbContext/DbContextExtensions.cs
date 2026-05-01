using Vehicle.Insurance.Infra.Data.Sql.Commands.Common;
using Vehicle.Insurance.Infra.Data.Sql.Commands.Common.Interceptors;
using Vehicle.Insurance.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.DependencyInjection.DbContext.CacheKeyFactory;
using Zamin.Infra.Data.Sql.Commands.Interceptors;

namespace Vehicle.Insurance.Endpoints.HostedService.Infrastructures.DependencyInjection.DbContext;

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
        services.AddDbContextFactory<VehicleInsuranceCommandDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("CommandDb_ConnectionString"))
                //.LogTo(Console.WriteLine, LogLevel.Information)
                .AddInterceptors(new SetPersianYeKeInterceptor(),
                                 new AddAuditDataInterceptor(),
                                 new AddRelatedEntitiesIdInterceptor());
            options.ReplaceService<IModelCacheKeyFactory, TenantModelCommandCacheKeyFactory>();
        });

        services.AddScoped<IVehicleInsuranceCommandDbContextFactory, VehicleInsuranceCommandDbContextFactory>();

        services.AddScoped(serviceProvider =>
        {
            var factory = serviceProvider.GetService<IVehicleInsuranceCommandDbContextFactory>();
            return factory.CreateDbContext();
        });

        //QueryDbContext
        services.AddDbContextFactory<VehicleInsuranceQueryDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("QueryDb_ConnectionString"));
            //.LogTo(Console.WriteLine, LogLevel.Information);
            options.ReplaceService<IModelCacheKeyFactory, TenantModelQueryCacheKeyFactory>();
        });

        services.AddScoped<IVehicleInsuranceQueryDbContextFactory, VehicleInsuranceQueryDbContextFactory>();

        services.AddScoped(serviceProvider =>
        {
            var factory = serviceProvider.GetService<IVehicleInsuranceQueryDbContextFactory>();
            return factory.CreateDbContext();
        });

        return services;
    }
}
