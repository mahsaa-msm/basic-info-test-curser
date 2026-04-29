using Vehicle.Insurance.Core.Contracts.Common.Options;
using Vehicle.Insurance.Core.Contracts.Common.Services.Tenant;
using Vehicle.Insurance.Core.Contracts.PodSsoApis.UserInfo;
using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.DependencyInjection.DbContext;
using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.DependencyInjection.IdentityServer.Options;
using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Extensions.Grpc;
using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Extensions.HttpClient;
using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.BackgroundJob.Models;
using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.Tenant;
using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.UserInfo;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Serilog;
using Zamin.Extensions.DependencyInjection;
using Zamin.Extensions.UsersManagement.Abstractions;

namespace Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Extensions;

public static class HostingExtensions
{
    public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddEnvironmentVariables();
        #region Bind OauthOption Option
        OAuthOption oAuthOption = new();
        builder.Configuration.Bind("OAuth", oAuthOption);
        builder.Services.AddSingleton(oAuthOption);
        #endregion

        #region Bind GrpcOption
        GrpcOption grpcOption = new();
        builder.Configuration.Bind(nameof(grpcOption), grpcOption);
        builder.Services.AddSingleton(grpcOption);
        #endregion

        #region Bind SoftwareManagementOption
        SoftwareManagementOption softwareManagementOption = new();
        builder.Configuration.Bind(nameof(softwareManagementOption), softwareManagementOption);
        builder.Services.AddSingleton(softwareManagementOption);
        #endregion

        #region Bind CoreSsoOptions Option
        CoreSsoOptions coreSsoOptions = new();
        builder.Configuration.Bind(nameof(coreSsoOptions), coreSsoOptions);
        builder.Services.AddSingleton(coreSsoOptions);
        #endregion

        #region Bind CoreInsuranceOption Option
        CoreInsuranceOption coreInsuranceOption = new();
        builder.Configuration.Bind(nameof(coreInsuranceOption), coreInsuranceOption);
        builder.Services.AddSingleton(coreInsuranceOption);
        #endregion

        #region Bind VehicleInsuranceOptions Option
        VehicleInsuranceOptions masterDataOptions = new();
        builder.Configuration.Bind(nameof(masterDataOptions), masterDataOptions);
        builder.Services.AddSingleton(masterDataOptions);
        #endregion

        #region Bind JobSchedulerOption
        builder.Services.Configure<JobScheduleOption>(
            builder.Configuration.GetSection(nameof(JobScheduleOption))
        );
        #endregion

        return builder;
    }

    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        //zamin
        builder.Services.AddZaminApiCore("Zamin", "Vehicle.Insurance");

        //microsoft
        builder.Services.AddEndpointsApiExplorer();

        //zamin
        builder.Services.AddZaminWebUserInfoService(builder.Configuration.GetSection("UserManagement"), false);

        //zamin
        builder.Services.AddZaminParrotTranslator(builder.Configuration.GetSection("ParrotTranslator"));

        //zamin
        builder.Services.AddZaminAutoMapperProfiles(builder.Configuration.GetSection("AutoMapper"));

        //zamin
        builder.Services.AddZaminRedisDistributedCache(builder.Configuration, "DistributedRedisCache");

        //zamin
        builder.Services.AddZaminNewtonSoftSerializer();

        //DbContext
        builder.Services.AddDbContexts(builder.Configuration);

        //Register External Apis
        builder.Services.AddExternalApiServices(builder.Configuration);

        builder.Services.AddGrpcClients();

        //jobs
        builder.Services.AddJobServices();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<IModernUserInfoService, ModernUserInfoService>();
        builder.Services.AddTransient<IUserInfoService, ModernUserInfoService>();
        builder.Services.AddTransient<ITenantService, TenantService>();
        builder.Services.AddSingleton<ITenantResolver, TenantResolver>();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        //zamin
        app.UseZaminApiExceptionHandler();

        //Serilog
        app.UseSerilogRequestLogging();

        app.UseCors(delegate (CorsPolicyBuilder builder)
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });


        return app;
    }
}

