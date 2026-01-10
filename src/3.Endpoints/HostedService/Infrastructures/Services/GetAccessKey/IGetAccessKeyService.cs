using Master.Data.Endpoints.HostedService.Infrastructures.Services.GetAccessList;
using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Master.Data.Endpoints.HostedService.Infrastructures.Services.GetAccessKey
{
    public interface IGetAccessKeyService : ITransientLifetime
    {
        Task<AccessModel?> ExecuteAsync(string actionName);
    }
}
