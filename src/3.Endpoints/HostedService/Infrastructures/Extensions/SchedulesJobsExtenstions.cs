using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.BackgroundJob;
using System.Reflection;

namespace Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Extensions;

public static class SchedulesJobsExtenstions
{
    public static IServiceCollection AddJobServices(this IServiceCollection services)
    {
        var jobType = typeof(BaseBackgroundJob<,>);
        var assembly = Assembly.GetExecutingAssembly();

        var jobTypes = assembly.GetTypes()
        .Where(t => t.IsClass && !t.IsAbstract && t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == jobType);

        foreach (var type in jobTypes)
        {
            var addHostedServiceMethod = typeof(ServiceCollectionHostedServiceExtensions)
            .GetMethods(BindingFlags.Static | BindingFlags.Public)
            .First(m => m.Name == "AddHostedService" && m.IsGenericMethod);

            var genericMethod = addHostedServiceMethod.MakeGenericMethod(type);
            genericMethod.Invoke(null, new object[] { services });
        }

        return services;
    }
}
