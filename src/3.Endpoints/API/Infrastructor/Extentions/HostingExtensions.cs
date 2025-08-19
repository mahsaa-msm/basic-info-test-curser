using Master.Data.Core.Contracts.Common.Options;
using Master.Data.Core.Contracts.PodSsoApis.UserInfo;
using Master.Data.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer.Extentions;
using Master.Data.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer.Options;
using Master.Data.Endpoints.API.Infrastructor.DependencyInjection.Swaggers.Extentions;
using Master.Data.Endpoints.API.Infrastructor.Extentions.Grpc;
using Master.Data.Endpoints.API.Infrastructor.Services.UserInfo;
using Master.Data.Infra.Data.Sql.Commands.Common;
using Master.Data.Infra.Data.Sql.Queries.Common;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
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
        //builder.Services.AddZaminInMemoryCaching();
        //builder.Services.AddZaminSqlDistributedCache(configuration, "SqlDistributedCache");
        builder.Services.AddZaminRedisDistributedCache(builder.Configuration, "DistributedRedisCache");

        //CommandDbContext
        builder.Services.AddDbContext<MasterDataCommandDbContext>(c => c.UseSqlServer(configuration.GetConnectionString("CommandDb_ConnectionString"))
            .AddInterceptors(new SetPersianYeKeInterceptor(), new AddAuditDataInterceptor()));

        //QueryDbContext
        builder.Services.AddDbContext<MasterDataQueryDbContext>(c => c.UseSqlServer(configuration.GetConnectionString("QueryDb_ConnectionString")));

        builder.Services.AddIdentityServer(builder.Configuration, "OAuth");

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

        var useIdentityServer = app.UseIdentityServer("OAuth");
        if (useIdentityServer)
            controllerBuilder.RequireAuthorization();

        app.Services.GetService<SoftwarePartDetectorService>()?.Run();

        return app;
    }
}