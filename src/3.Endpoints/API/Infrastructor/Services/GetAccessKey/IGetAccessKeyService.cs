using Master.Data.Endpoints.API.Infrastructor.Services.GetAccessList;
using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Master.Data.Endpoints.API.Infrastructor.Services.GetAccessKey
{
    public interface IGetAccessKeyService : ITransientLifetime
    {
        Task<AccessModel?> ExecuteAsync(string actionName);
    }
}
