using Master.Data.Core.Contracts.ExternalAPI.Common.Configs;
using Master.Data.Infra.ExternalApi.CoreInsurance.Authentication;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Master.Data.Endpoints.API.CoreInsuranceServices.Handlers;

public class CoreInsuranceAuthHeaderHandler : DelegatingHandler
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMemoryCache _memoryCache;
    private readonly APICoreInsuranceConfig _coreConfig;

    public CoreInsuranceAuthHeaderHandler(
        IServiceProvider serviceProvider,
        IMemoryCache memoryCache,
        IOptionsMonitor<APICoreInsuranceConfig> coreConfig)
    {
        _serviceProvider = serviceProvider;
        _memoryCache = memoryCache;
        _coreConfig = coreConfig.CurrentValue;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (!IsTokenFromCache(out string token))
        {
            using var scope = _serviceProvider.CreateScope();
            var _coreAuth = scope.ServiceProvider.GetRequiredService<ICoreInsuranceAuthentication>();
            token = await _coreAuth.GetToken();
            SetTokenInCache(token);
        }

        if (!request.Headers.Contains("Cookie"))
        {
            request.Headers.Add("Cookie", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }

    private bool IsTokenFromCache(out string token)
    {
        token = string.Empty;
        if (_memoryCache.TryGetValue(_coreConfig.LoginTokenName, out string cacheToken))
        {
            token = cacheToken;
            return true;
        }
        return false;
    }

    private void SetTokenInCache(string token)
    {
        if (!string.IsNullOrEmpty(token))
        {
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2)
            };
            _memoryCache.Set(_coreConfig.LoginTokenName, token, cacheOptions);
        }
    }
}
