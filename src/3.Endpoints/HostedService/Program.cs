using Master.Data.Endpoints.HostedService.Extensions;
using Zamin.Extensions.DependencyInjection;
using Zamin.Utilities.SerilogRegistration.Extensions;

SerilogExtensions.RunWithSerilogExceptionHandling(() =>
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Configuration.AddEnvironmentVariables();

    builder.AddZaminSerilog(o =>
    {
        o.ApplicationName = builder.Configuration.GetValue<string>("ApplicationName");
        o.ServiceName = builder.Configuration.GetValue<string>("ServiceName");
        o.ServiceId = builder.Configuration.GetValue<string>("ServiceId");
        o.ServiceVersion = builder.Configuration.GetValue<string>("ServiceVersion");
    }).ConfigureServices().ConfigurePipeline().Run();
});
