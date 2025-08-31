using Master.Data.Core.Contracts.Common.Options;
using Master.Data.Core.Contracts.CoreSsoApis.Queries;
using Master.Data.Core.RequestResponse.Common.Extensions;
using Master.Data.Core.RequestResponse.Common.Requests;
using Master.Data.Core.RequestResponse.CoreSsoApis.Queries.GetTtoken;
using Master.Data.Core.Resources;
using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Master.Data.Infra.ExternalApi.CoreSso.Callers;
public sealed class CoreSsoGetTokenCaller : ICoreSsoGetTokenCaller, ITransientLifetime
{
    private readonly HttpClient _httpClient;
    private readonly CoreSsoOptions _coreSsoOptions;

    public CoreSsoGetTokenCaller(IHttpClientFactory httpClientfactory,
                                 CoreSsoOptions coreSsoOptions)
    {
        _httpClient = httpClientfactory.CreateClient(ProjectConsts.CORE_SSO_HTTP_CLIENT_NAME);
        _coreSsoOptions = coreSsoOptions;
    }

    public async Task<Response<CoreSsoGetTokenResponse>> Call(CoreSsoGetTokenRequest request)
    {
        var formData = new HashSet<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string,string>("grant_type", _coreSsoOptions.GrantType),
            new KeyValuePair<string,string>("client_id", request.ClientId ?? _coreSsoOptions.ClientId),
            new KeyValuePair<string,string>("client_secret", request.ClientSecret ?? _coreSsoOptions.ClientSecret),
        };

        request.Scopes?.ForEach(s =>
        {
            formData.Add(new KeyValuePair<string, string>("scope", s));
        });

        if (request.UseDefaultScopes)
            _coreSsoOptions.Scopes?.ForEach(s =>
            {
                formData.Add(new KeyValuePair<string, string>("scope", s));
            });

        var requestContent = new FormUrlEncodedContent(formData);

        var baseUri = new Uri(_coreSsoOptions.BasePath ?? "");
        var requestUri = new Uri(baseUri, $"{request.Path}");

        var response = await _httpClient.PostAsync(requestUri, requestContent);

        return await response.ToResultAsync<CoreSsoGetTokenResponse>();
    }
}