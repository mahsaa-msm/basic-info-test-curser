using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.GetAccessList;
using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.GetAccessKey
{
    public interface IGetAccessKeyService : ITransientLifetime
    {
        Task<AccessModel?> ExecuteAsync(string actionName);
    }
}

