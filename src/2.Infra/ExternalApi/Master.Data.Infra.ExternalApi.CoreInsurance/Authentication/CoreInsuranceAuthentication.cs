using Master.Data.Core.Contracts.ExternalAPI.Authentication.Request;
using Master.Data.Core.Contracts.ExternalAPI.Common.Configs;
using Master.Data.Core.Resources;
using Master.Data.Infra.ExternalApi.CoreInsurance.Contracts;
using Microsoft.Extensions.Options;
using Refit;
using Zamin.Core.Domain.Exceptions;

namespace Master.Data.Infra.ExternalApi.CoreInsurance.Authentication;
public class CoreInsuranceAuthentication : ICoreInsuranceAuthentication
{
    private readonly IServiceProvider _serviceProvider;
    private readonly APICoreInsuranceConfig _coreInsuranceConfig;

    public CoreInsuranceAuthentication(IOptionsMonitor<APICoreInsuranceConfig> coreInsuranceConfig,
        IServiceProvider serviceProvider)
    {
        _coreInsuranceConfig = coreInsuranceConfig.CurrentValue;
        _serviceProvider = serviceProvider;
    }

    public async Task<string> GetToken()
    {
        var httpClient = CreateHttpClientWithSslIgnoreAndBaseAddress();
        var _coreAuthClient = RestService.For<ICoreAuthClient>(httpClient);

        LoginRequestModel model = new() { username = _coreInsuranceConfig.UserName, password = _coreInsuranceConfig.Password };
        var responseLogin = await _coreAuthClient.Login(model);
        if (responseLogin.Content is null)
        {
            throw new InvalidEntityStateException(ProjectValidationError.ERROR_IN_GET_TOKEN,
                 ProjectTranslation.CORE_API_TOKEN);
        }
        return GetLoginHeader(responseLogin);
    }

    private static string GetLoginHeader(ApiResponse<string> input)
    {
        if (!input.Headers.TryGetValues("Set-Cookie", out IEnumerable<string> values) || values == null)
            return string.Empty;

        var cookieList = values
            .Select(cookie => cookie.Split(';')[0])
            .Where(kv => !string.IsNullOrWhiteSpace(kv));

        return string.Join("; ", cookieList);
    }



    private HttpClient CreateHttpClientWithSslIgnoreAndBaseAddress()
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => _coreInsuranceConfig.IgnoreSSL
        };

        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri(_coreInsuranceConfig.BaseAddress)
        };

        return httpClient;
    }
}
