using Master.Data.Core.Contracts.Common.Options;
using Master.Data.Core.Contracts.Common.Services.Tenant;
using Master.Data.Core.Contracts.ExternalAPI.Common.Configs;
using Master.Data.Core.Contracts.PodSsoApis.UserInfo;
using Master.Data.Endpoints.API.CoreInsuranceServices.Handlers;
using Master.Data.Endpoints.API.Infrastructor.DependencyInjection.DbContext;
using Master.Data.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer.Extentions;
using Master.Data.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer.Options;
using Master.Data.Endpoints.API.Infrastructor.DependencyInjection.Swaggers.Extentions;
using Master.Data.Endpoints.API.Infrastructor.Extentions.Grpc;
//using Master.Data.Endpoints.API.Infrastructor.Middlewares;
using Master.Data.Endpoints.API.Infrastructor.Services.Tenant;
using Master.Data.Endpoints.API.Infrastructor.Services.UserInfo;
using Master.Data.Endpoints.API.Infrastructure.Services.Tenant;
using Master.Data.Infra.Data.Sql.Commands.Common.Interceptors;
using Master.Data.Infra.ExternalApi.CoreInsurance.Contracts;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Refit;
using Serilog;
using Zamin.EndPoints.Web.Extensions.ModelBinding;
using Zamin.Extensions.DependencyInjection;
using Zamin.Extensions.UsersManagement.Abstractions;
using Zamin.Infra.Data.Sql.Commands.Interceptors;
using Zamin.Utilities.SoftwarePartDetector.Services;

namespace Master.Data.Endpoints.API.Infrastructor.Extentions;

public static class HostingExtensions
{
    public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
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

        return builder;
    }

    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        IConfiguration configuration = builder.Configuration;

        //zamin
        builder.Services.AddZaminApiCore("Zamin", "Master.Data");

        //microsoft
        builder.Services.AddEndpointsApiExplorer();

        //zamin
        builder.Services.AddZaminWebUserInfoService(builder.Configuration.GetSection("UserManagement"), false);

        //zamin
        builder.Services.AddZaminParrotTranslator(builder.Configuration.GetSection("ParrotTranslator"));

        //zamin
        builder.Services.AddSoftwarePartDetector(builder.Configuration.GetSection("SoftwarePart"));

        //zamin
        builder.Services.AddZaminAutoMapperProfiles(builder.Configuration.GetSection("AutoMapper"));

        //zamin
        builder.Services.AddNonValidatingValidator();

        //zamin
        //builder.Services.AddZaminMicrosoftSerializer();
        builder.Services.AddZaminNewtonSoftSerializer();

        //zamin
        builder.Services.AddZaminInMemoryCaching();
        //builder.Services.AddZaminSqlDistributedCache(configuration, "SqlDistributedCache");
        builder.Services.AddZaminRedisDistributedCache(builder.Configuration, "DistributedRedisCache");

        //DbContext
        builder.Services.AddDbContexts(builder.Configuration);

        builder.Services.AddIdentityServer(builder.Configuration, "OAuth");

        var apiCoreConfig = builder.Configuration.GetSection(nameof(APICoreInsuranceConfig)).Get<APICoreInsuranceConfig>();

        builder.Services
           .AddRefitClient<ICoreInsuranceClient>(new RefitSettings()
           {
               ContentSerializer = new NewtonsoftJsonContentSerializer()
           })
           .ConfigureHttpClient(c =>
           {
               c.Timeout = TimeSpan.FromSeconds(200);
               c.BaseAddress = new Uri(apiCoreConfig.BaseAddress);
           }).ConfigurePrimaryHttpMessageHandler(serviceProvider =>
           {
               var handler = new HttpClientHandler();
               if (apiCoreConfig.IgnoreSSL)
               {
                   handler.ServerCertificateCustomValidationCallback =
                       HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
               }
               return handler;
           })
           .AddHttpMessageHandler<CoreInsuranceAuthHeaderHandler>();

        builder.Services.Configure<APICoreInsuranceConfig>(builder.Configuration.GetSection(nameof(APICoreInsuranceConfig)));
        builder.Services.Configure<NewAPICoreInsuranceConfig>(builder.Configuration.GetSection(nameof(NewAPICoreInsuranceConfig)));

        //PollingPublisher
        //builder.Services.AddZaminPollingPublisherDalSql(configuration, "PollingPublisherSqlStore");
        //builder.Services.AddZaminPollingPublisher(configuration, "PollingPublisher");

        //MessageInbox
        //builder.Services.AddZaminMessageInboxDalSql(configuration, "MessageInboxSqlStore");
        //builder.Services.AddZaminMessageInbox(configuration, "MessageInbox");

        //builder.Services.AddZaminRabbitMqMessageBus(configuration, "RabbitMq");

        //builder.Services.AddZaminTraceJeager(configuration, "OpenTeletmetry");

        builder.Services.AddGrpcClients();

        builder.Services.AddSwagger(builder.Configuration, "Swagger");

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<IModernUserInfoService, ModernUserInfoService>();
        builder.Services.AddTransient<IUserInfoService, ModernUserInfoService>();
        builder.Services.AddTransient<SetPersianYeKeInterceptor>();
        builder.Services.AddTransient<AddAuditDataInterceptor>();
        builder.Services.AddTransient<AddRelatedEntitiesIdInterceptor>();
        builder.Services.AddTransient<ITenantService, TenantService>();
        builder.Services.AddSingleton<ITenantResolver, TenantResolver>();
        builder.Services.AddTransient<CoreInsuranceAuthHeaderHandler>();
        builder.Services.AddTransient<NewCoreInsuranceAuthHeaderHandler>();
        builder.Services.AddSwaggerGen();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        //zamin
        app.UseZaminApiExceptionHandler();

        //Serilog
        app.UseSerilogRequestLogging();

        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI();
        //}

        app.UseSwaggerUI("Swagger");

        app.UseStatusCodePages();

        app.UseCors(delegate (CorsPolicyBuilder builder)
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });

        app.UseHttpsRedirection();


        //app.MapProjectGrpcServices();

        //app.Services.ReceiveEventFromRabbitMqMessageBus(new KeyValuePair<string, string>("MiniAggregateName", "AggregateNameCreated"));

        var controllerBuilder = app.MapControllers();

        //app.UseMiddleware<TenantMiddleware>();

        var useIdentityServer = app.UseIdentityServer("OAuth");
        if (useIdentityServer)
            controllerBuilder.RequireAuthorization();

        PrintEnvironmentSettings(app);

        app.Services.GetService<SoftwarePartDetectorService>()?.Run();

        return app;
    }
    private static void PrintEnvironmentSettings(WebApplication app)
    {
        var env = app.Environment;
        var logger = app.Services.GetRequiredService<ILogger<WebApplication>>();

        logger.Log(LogLevel.Warning, $"*************|EnvironmentName: {env.EnvironmentName}");
        logger.Log(LogLevel.Warning, $"*************|IsDevelopment(): {env.IsDevelopment()}");
        logger.Log(LogLevel.Warning, $"*************|IsProduction(): {env.IsProduction()}");
        logger.Log(LogLevel.Warning, $"*************|IsTest(): {env.EnvironmentName.Equals("Test", StringComparison.OrdinalIgnoreCase)}");
        logger.Log(LogLevel.Warning, $"*************|IsStaging(): {env.IsStaging()}");
    }

    public static WebApplicationBuilder AddEnvironment(this WebApplicationBuilder builder)
    {
        var envName = builder.Configuration["EnvironmentName"];
        if (!string.IsNullOrEmpty(envName))
        {
            builder.Environment.EnvironmentName = envName;
        }
        return builder;
    }
}