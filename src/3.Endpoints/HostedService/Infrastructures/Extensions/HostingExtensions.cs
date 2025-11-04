using Master.Data.Endpoints.HostedService.Infrastructures.Services.BackgroundJob.Models;
using Zamin.Extensions.DependencyInjection;

namespace Master.Data.Endpoints.HostedService.Infrastructures.Extensions;

public static class HostingExtensions
{

    public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddEnvironmentVariables();

        #region Bind JobSchedulerOption
        JobScheduleOption jobScheduleOption = new();
        builder.Configuration.Bind(nameof(jobScheduleOption), jobScheduleOption);
        builder.Services.AddSingleton(jobScheduleOption);
        #endregion

        return builder;
    }


    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {

        #region Add DbContexts

        #endregion

        //microsoft
        builder.Services.AddEndpointsApiExplorer();

        //zamin
        builder.Services.AddZaminApiCore("Zamin", "Master.Data");

        //zamin
        builder.Services.AddZaminWebUserInfoService(builder.Configuration.GetSection("UserManagement"), false);

        //zamin
        builder.Services.AddZaminParrotTranslator(builder.Configuration.GetSection("ParrotTranslator"));

        //zamin
        builder.Services.AddZaminNewtonSoftSerializer();

        //zamin
        builder.Services.AddZaminAutoMapperProfiles(builder.Configuration.GetSection("AutoMapper"));

        //zamin
        builder.Services.AddZaminRedisDistributedCache(builder.Configuration, "DistributedRedisCache");

        //jobs
        builder.Services.AddJobServices();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        return app;
    }
}
