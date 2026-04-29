using IdentityModel.Client;
using Vehicle.Insurance.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer.Models;
using Vehicle.Insurance.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer.Options;
using Newtonsoft.Json;
using System.Dynamic;
using System.Security.Claims;
using Zamin.Extensions.Caching.Abstractions;

namespace Vehicle.Insurance.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer;

public static class ProviderExtensions
{

    public static IServiceCollection AddProviderHttpClient(this IServiceCollection services, OAuthOption oAuthOption)
    {
        IHttpClientBuilder httpClientBuilder = services.AddHttpClient("ProviderHttpClient", option => { option.BaseAddress = new Uri(oAuthOption.Authority); });
        if (oAuthOption.IgnoreSSL)
        {
            httpClientBuilder.ConfigurePrimaryHttpMessageHandler(
                () => new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = delegate { return true; }
                });
        }

        return services;
    }

    public static async Task<List<Claim>> GetUserInfoClaims(this OAuthOption oAuthOption, HttpContext httpContext, string httpClientName)
    {
        var token = httpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "")
            ?? throw new ArgumentNullException($"{oAuthOption.TokenType.ToString()} ({oAuthOption.Authority}) , token is null");

        var client = httpContext.RequestServices.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientName);

        var userInfoClaims = await oAuthOption.UserInfoEndpointCaller(httpContext, client, token);

        return userInfoClaims;
    }

    public static async Task<List<Claim>> UserInfoEndpointCaller(this OAuthOption oAuthOption, HttpContext httpContext, System.Net.Http.HttpClient client, string token)
    {
        List<Claim> claims = [];
        if (oAuthOption.RegisterUserInfoClaims.CachingData)
        {
            var cacheAdapter = httpContext.RequestServices.GetRequiredService<ICacheAdapter>()
                ?? throw new ArgumentNullException($"{oAuthOption.TokenType.ToString()} ({oAuthOption.Authority}) , cache adapter is null");

            string cacheKey = oAuthOption.GenerateCacheKey(token);

            claims = cacheAdapter.Get<List<ClaimCacheModel>>(cacheKey)?.Select(cacheModel => cacheModel.ToClaim()).ToList() ?? [];
            if (claims is null || claims.Count == 0)
            {
                claims = await oAuthOption.CallUserInfoEndpoint(client, token);
                switch (oAuthOption.RegisterUserInfoClaims.CacheExpirationType)
                {
                    case CacheExpirationType.Absolute:
                        cacheAdapter.Add(cacheKey,
                                         claims.Select(ClaimCacheModel.FromClaim),
                                         DateTime.Now.AddSeconds(oAuthOption.RegisterUserInfoClaims.CacheExpirationInSeconds),
                                         null);
                        break;
                    case CacheExpirationType.Sliding:
                        cacheAdapter.Add(cacheKey,
                                         claims.Select(ClaimCacheModel.FromClaim),
                                         null,
                                         TimeSpan.FromSeconds(oAuthOption.RegisterUserInfoClaims.CacheExpirationInSeconds));
                        break;
                    default:
                        throw new InvalidOperationException($"Invalid cache expiration type for {oAuthOption.TokenType.ToString()} ({oAuthOption.Authority})");
                }
            }
        }
        else
        {

            claims = await oAuthOption.CallUserInfoEndpoint(client, token);
        }

        return claims;
    }

    private static string GenerateCacheKey(this OAuthOption oAuthOption, string token)
    {
        var cacheKeyText = $"{oAuthOption.RegisterUserInfoClaims.CacheKeyPrefix}{oAuthOption.TokenType.ToString()}_{token}";
        var cacheKey = oAuthOption.RegisterUserInfoClaims.CacheKeyFormat == CacheKeyFormat.Base64 ? cacheKeyText.ToBase64Encode() : cacheKeyText;
        return cacheKey;
    }

    public static async Task<List<Claim>> CallUserInfoEndpoint(this OAuthOption oAuthOption, System.Net.Http.HttpClient client, string token)
    {
        var response = await client.GetUserInfoAsync(new UserInfoRequest
        {
            Address = oAuthOption.EndpointsPath?.UserInfoEndpoint ?? (await client.GetDiscoveryDocumentAsync())?.UserInfoEndpoint?.Replace(oAuthOption.Authority, ""),
            Token = token
        });


        if (response.IsError) throw new Exception(response.Error);


        var claimsList = response.Claims.ToList();

        var userMetaData = claimsList.FirstOrDefault(x => x.Type == "user_metadata");
        if (userMetaData is not null)
        {
            dynamic data = JsonConvert.DeserializeObject<ExpandoObject>(userMetaData.Value);
            claimsList.AddRange(ConvertToClaims(data));
        }

        var userClientMetaData = claimsList.FirstOrDefault(x => x.Type == "client_metadata");
        if (userClientMetaData is not null)
        {
            dynamic data = JsonConvert.DeserializeObject<ExpandoObject>(userClientMetaData.Value);
            claimsList.AddRange(ConvertToClaims(data));
        }

        return [.. claimsList];
    }

    public static List<Claim> ConvertToClaims(dynamic data)
    {
        var claims = new List<Claim>();

        var dic = data as IDictionary<string, object>;
        if (dic is not null)
        {
            foreach (var property in dic)
            {
                string claimType = property.Key;
                string claimValue = property.Value?.ToString() ?? string.Empty;
                claims.Add(new Claim(claimType, claimValue));
            }
        }

        return claims;
    }
    private static string ToBase64Encode(this string plainText) => Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plainText));
}
