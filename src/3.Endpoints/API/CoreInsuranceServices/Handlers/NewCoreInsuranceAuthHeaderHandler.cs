using Master.Data.Core.Contracts.ExternalAPI.Common.Configs;
using Master.Data.Infra.ExternalApi.CoreInsurance.Authentication;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Master.Data.Endpoints.API.CoreInsuranceServices.Handlers;

public class NewCoreInsuranceAuthHeaderHandler : DelegatingHandler
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMemoryCache _memoryCache;
    private readonly NewAPICoreInsuranceConfig _newAPICoreInsuranceConfig;
    private readonly IHttpClientFactory _httpClientFactory;

    public NewCoreInsuranceAuthHeaderHandler(
        IServiceProvider serviceProvider,
        IMemoryCache memoryCache,
        IOptionsMonitor<NewAPICoreInsuranceConfig> newAPICoreInsuranceConfig,
        IHttpClientFactory httpClientFactory)
    {
        _serviceProvider = serviceProvider;
        _memoryCache = memoryCache;
        _newAPICoreInsuranceConfig = newAPICoreInsuranceConfig.CurrentValue;
        _httpClientFactory = httpClientFactory;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (!IsTokenFromCache(out string token))
        {
            using var scope = _serviceProvider.CreateScope();
            var _coreAuth = scope.ServiceProvider.GetRequiredService<INewCoreInsuranceAuthentication>();
            token = await _coreAuth.GetToken().ConfigureAwait(false);
            SetTokenInCache(token);
        }

        request.Headers.Add(_newAPICoreInsuranceConfig.TokenKey, token);
        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }

    private bool IsTokenFromCache(out string token)
    {
        token = string.Empty;
        if (_memoryCache.TryGetValue(_newAPICoreInsuranceConfig.TokenKey, out string cacheToken))
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
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };
            _memoryCache.Set(_newAPICoreInsuranceConfig.TokenKey, token, cacheOptions);
        }
    }
}
