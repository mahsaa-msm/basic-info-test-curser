using Vehicle.Insurance.Endpoints.API.Infrastructor.Services.GetAccessList;
using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Vehicle.Insurance.Endpoints.API.Infrastructor.Services.GetAccessKey
{
    public interface IGetAccessKeyService : ITransientLifetime
    {
        Task<AccessModel?> ExecuteAsync(string actionName);
    }
}

