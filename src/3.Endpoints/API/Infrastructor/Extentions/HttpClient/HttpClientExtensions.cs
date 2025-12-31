using Master.Data.Core.Contracts.Common.Options;
using Master.Data.Core.Resources;
using Master.Data.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer.Options;
using Master.Data.Endpoints.API.Infrastructor.Extentions.HttpClient.Handlers;
using Master.Data.Endpoints.API.Infrastructor.Extentions.HttpClient.Policies;

namespace Master.Data.Endpoints.API.Infrastructor.Extentions.HttpClient;

public static class HttpClientExtensions
{
    public static IServiceCollection AddExternalApiServices(this IServiceCollection services,
                                                            IConfiguration configuration)
    {
        #region Bind PollyOptions Option
        var pollyOptions = configuration.GetSection("PollyOptions");
        services.Configure<PollyOptions>(pollyOptions);
        #endregion

        services.AddTransient<LoggingHandler>();
        services.AddTransient<ExceptionHandler>();
        // استفاده از این هندلر منطق دریافت توکن و اضافه کردن آن به هدر درخواست را انجام میدهد.
        // این برای درخواست هایی که با sso کور کار میکنند کار خواهد کرد.
        // برای استفاده باید در تعریف httpClient مد نظر، این هندلر را به عنوان اولین messageHandler اضافه کنیم
        // .AddHttpMessageHandler<CoreSsoTokenHandler>()
        // در صورتی که بخشی از api های کور را نیاز داریم که توکن در آن ها نیازی نیست،
        // میتوانیم یک http client دیگر بدون این message handler برای آن ها ایجاد کنیم.
        // این برای همه هندلر ها یا حتی polly نیز صدق میکند و در کنار انعطاف، سربار خاصی اعمال نخواهد کرد.
        services.AddTransient<CoreSsoTokenHandler>();
        services.AddTransient<CoreTokenHandler>();

        services.AddSingleton<RetryPolicies>();
        services.AddSingleton<CircuitBreakerPolicies>();
        services.AddSingleton<TimeoutPolicies>(); //must be latest

        #region ACL
        services.AddHttpClient(ProjectConsts.ACL_HTTP_CLIENT_NAME, (serviceProvider, httpClient) =>
        {
            var oAuthOption = serviceProvider.GetRequiredService<OAuthOption>();
            httpClient.BaseAddress = new Uri(oAuthOption.AuthorizationConfigs.AclBaseUrl ?? "");
        })
        .AddHttpMessageHandler<LoggingHandler>()
        .AddPolicyHandler((provider, request) => provider.GetRequiredService<RetryPolicies>().BasicRetryPolicy)
        .AddPolicyHandler((provider, request) => provider.GetRequiredService<CircuitBreakerPolicies>().BasicCircuitBreakerPolicy)
        .AddPolicyHandler((provider, request) => provider.GetRequiredService<TimeoutPolicies>().DynamicTimeoutPolicy)
        .AddHttpMessageHandler<ExceptionHandler>();
        #endregion

        #region CoreSSO
        services.AddHttpClient(ProjectConsts.CORE_SSO_HTTP_CLIENT_NAME, (serviceProvider, httpClient) =>
        {
            var coreSsoOption = serviceProvider.GetRequiredService<CoreSsoOptions>();
            httpClient.BaseAddress = new Uri(coreSsoOption.BasePath ?? "");
        })
        .ConfigurePrimaryHttpMessageHandler((serviceProvider) =>
        {
            var coreSsoOption = serviceProvider.GetRequiredService<CoreSsoOptions>();
            return coreSsoOption.IgnoreSslCheck ?
            new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
            }
            : new HttpClientHandler();
        })
        .AddHttpMessageHandler<LoggingHandler>()
        .AddPolicyHandler((provider, request) => provider.GetRequiredService<RetryPolicies>().BasicRetryPolicy)
        .AddPolicyHandler((provider, request) => provider.GetRequiredService<CircuitBreakerPolicies>().BasicCircuitBreakerPolicy)
        .AddPolicyHandler((provider, request) => provider.GetRequiredService<TimeoutPolicies>().DynamicTimeoutPolicy)
        .AddHttpMessageHandler<ExceptionHandler>();
        #endregion

        #region CoreInsurance
        services.AddHttpClient(ProjectConsts.CORE_INSURANCE_HTTP_CLIENT_NAME, (serviceProvider, httpClient) =>
        {
            var coreInsuranceOption = serviceProvider.GetRequiredService<CoreInsuranceOption>();
            httpClient.BaseAddress = new Uri(coreInsuranceOption.BasePath ?? "");
        })
        .ConfigurePrimaryHttpMessageHandler((serviceProvider) =>
        {
            var coreInsuranceOption = serviceProvider.GetRequiredService<CoreInsuranceOption>();
            return coreInsuranceOption.IgnoreSslCheck ?
            new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
            }
            : new HttpClientHandler();
        })
        .AddHttpMessageHandler<LoggingHandler>()
        .AddHttpMessageHandler<CoreSsoTokenHandler>()
        .AddPolicyHandler((provider, request) => provider.GetRequiredService<RetryPolicies>().BasicRetryPolicy)
        .AddPolicyHandler((provider, request) => provider.GetRequiredService<CircuitBreakerPolicies>().BasicCircuitBreakerPolicy)
        .AddPolicyHandler((provider, request) => provider.GetRequiredService<TimeoutPolicies>().DynamicTimeoutPolicy)
        .AddHttpMessageHandler<ExceptionHandler>();
        #endregion

        return services;
    }
}
