using Master.Data.Endpoints.HostedService.Infrastructures.Services.GetAccessList;
using Zamin.Extensions.Caching.Abstractions;

namespace Master.Data.Endpoints.HostedService.Infrastructures.Services.GetAccessKey;
public class GetAccessKeyService : IGetAccessKeyService
{
    private readonly ICacheAdapter _cacheAdapter;
    private readonly IGetAccessListService _getAccessListService;
    private readonly string cacheKey = "AccessList";
    public GetAccessKeyService(ICacheAdapter cacheAdapter, IGetAccessListService getAccessListService)
    {
        _cacheAdapter = cacheAdapter;
        _getAccessListService = getAccessListService;
    }

    public async Task<AccessModel?> ExecuteAsync(string actionName)
    {
        var accessList = _cacheAdapter.Get<List<AccessModel>>(cacheKey);

        if (accessList is null)
        {
            accessList = await _getAccessListService.ExecuteAsync();
            _cacheAdapter.Add(cacheKey, accessList, DateTime.Now.AddDays(1), null);
        }
        actionName = $"Action_{actionName}";

        if (accessList is not null)
            return accessList.FirstOrDefault(x => x.NamePath == actionName);

        return null;
    }
}