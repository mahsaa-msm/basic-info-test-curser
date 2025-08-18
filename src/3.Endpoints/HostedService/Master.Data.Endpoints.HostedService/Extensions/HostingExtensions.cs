using Zamin.Extensions.DependencyInjection;

namespace Master.Data.Endpoints.HostedService.Extensions;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        IConfiguration configuration = builder.Configuration;

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

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        return app;
    }
}
