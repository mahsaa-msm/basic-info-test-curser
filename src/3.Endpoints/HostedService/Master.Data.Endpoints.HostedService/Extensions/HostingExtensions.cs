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


        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        return app;
    }
}
