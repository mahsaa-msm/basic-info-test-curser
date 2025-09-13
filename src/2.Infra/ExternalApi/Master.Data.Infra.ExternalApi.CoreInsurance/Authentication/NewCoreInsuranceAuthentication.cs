using Master.Data.Core.Contracts.ExternalAPI.Common.Configs;
using Master.Data.Core.Resources;
using Master.Data.Infra.ExternalApi.CoreInsurance.Contracts;
using Microsoft.Extensions.Options;
using Refit;
using Zamin.Core.Domain.Exceptions;

namespace Master.Data.Infra.ExternalApi.CoreInsurance.Authentication;

public class NewCoreInsuranceAuthentication : INewCoreInsuranceAuthentication
{
    private NewAPICoreInsuranceConfig _newAPICoreInsuranceConfig;
    private readonly IServiceProvider _serviceProvider;
    public NewCoreInsuranceAuthentication(IOptionsMonitor<NewAPICoreInsuranceConfig> newAPICoreInsuranceConfig,
        IServiceProvider serviceProvider)
    {
        _newAPICoreInsuranceConfig = newAPICoreInsuranceConfig.CurrentValue;
        _serviceProvider = serviceProvider;

    }

    public async Task<string> GetToken()
    {
        var httpClient = CreateHttpClientWithSslIgnoreAndBaseAddress();
        var _newCoreAuthClien = RestService.For<INewCoreAuthClient>(httpClient);

        var formData = new Dictionary<string, string>
         {
            { "grant_type", _newAPICoreInsuranceConfig.Grant_Type },
            { "client_id", _newAPICoreInsuranceConfig.Client_Id },
            { "client_secret", _newAPICoreInsuranceConfig.Client_Secret },
            { "scope", _newAPICoreInsuranceConfig.Scope }
         };
        var responseLogin = await _newCoreAuthClien.Login(formData);
        if (responseLogin.Content is null)
        {
            throw new InvalidEntityStateException(ProjectValidationError.ERROR_IN_GET_TOKEN,
                 ProjectTranslation.API_SSO_CORE_TOKEN);
        }
        return responseLogin.Content.AccessToken;
    }
    private HttpClient CreateHttpClientWithSslIgnoreAndBaseAddress()
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => _newAPICoreInsuranceConfig.IgnoreSSL
        };

        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri(_newAPICoreInsuranceConfig.BaseAddress)
        };

        return httpClient;
    }
}
