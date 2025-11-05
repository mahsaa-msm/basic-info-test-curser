using Master.Data.Core.Contracts.CoreSsoApis.Queries;
using Master.Data.Core.RequestResponse.CoreSsoApis.Queries.GetTtoken;
using Master.Data.Core.Resources;
using System.Net;
using Zamin.Extensions.Caching.Abstractions;

namespace Master.Data.Endpoints.HostedService.Infrastructures.Extensions.HttpClient.Handlers;

public sealed class SsoTokenHandler : DelegatingHandler
{
    private readonly ILogger<SsoTokenHandler> _logger;
    private readonly ICacheAdapter _cacheAdapter;
    private readonly ICoreSsoGetTokenCaller _coreSsoGetTokenCaller;

    public SsoTokenHandler(ILogger<SsoTokenHandler> logger,
                               ICacheAdapter cacheAdapter,
                               ICoreSsoGetTokenCaller coreSsoGetTokenCaller)
    {
        _logger = logger;
        _cacheAdapter = cacheAdapter;
        _coreSsoGetTokenCaller = coreSsoGetTokenCaller;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await GetTokenFromCacheOrApiAsync(cancellationToken);

        if (!string.IsNullOrEmpty(token))
            request.Headers.Add("Oauth-2", token);

        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
        string contentString = await response.Content.ReadAsStringAsync(cancellationToken);
        if (response.StatusCode == HttpStatusCode.Unauthorized || contentString.Contains("<body>"))
        {
            request.Headers.Remove("Oauth-2");
            token = await RenewToken(cancellationToken);
            if (!string.IsNullOrEmpty(token))
                request.Headers.Add("Oauth-2", token);

            response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized || contentString.Contains("<body>"))
            {
                _logger.LogDebug(ProjectTranslation.CORE_SSO_AUTHENTICATION_FAILED);
                throw new Exception("Core SSO Authentication Failed");
            }
        }

        return response;
    }

    private async Task<string?> GetTokenFromCacheOrApiAsync(CancellationToken cancellationToken)
    {
        var cachedToken = _cacheAdapter.Get<string>(ProjectConsts.CORE_SSO_TOKEN_CACHE_KEY);

        if (!string.IsNullOrEmpty(cachedToken))
            return cachedToken;

        var tokenResponse = await _coreSsoGetTokenCaller.Call(new CoreSsoGetTokenRequest());

        if (tokenResponse is not null &&
            tokenResponse.IsSuccess &&
            tokenResponse.Value.AccessToken is not null)
        {
            _cacheAdapter.Add(
          key: ProjectConsts.CORE_SSO_TOKEN_CACHE_KEY,
          obj: tokenResponse.Value.AccessToken,
          AbsoluteExpiration: DateTime.UtcNow.AddSeconds(tokenResponse.Value.ExpiresIn),
          SlidingExpiration: null);

            return tokenResponse.Value.AccessToken;
        }
        return null;
    }

    private async Task<string?> RenewToken(CancellationToken cancellationToken)
    {
        _cacheAdapter.RemoveCache(ProjectConsts.CORE_SSO_TOKEN_CACHE_KEY);

        return await GetTokenFromCacheOrApiAsync(cancellationToken);
    }
}
