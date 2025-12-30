using Master.Data.Core.RequestResponse.PodSsoApis.Queries.GetUserAccesses;
using Master.Data.Core.Resources;
using Master.Data.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer.Options;
using Newtonsoft.Json;
using System.Net;

namespace Master.Data.Endpoints.API.Infrastructor.Services.GetUserAccessKeys;
public class GetUserAccessKeysService : IGetUserAccessKeysService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly OAuthOption _oAuthOption;
    public GetUserAccessKeysService(IHttpClientFactory httpClientFactory,
                                    OAuthOption oAuthOption)
    {
        _httpClientFactory = httpClientFactory;
        _oAuthOption = oAuthOption;
    }

    public async Task<List<string>> ExecuteAsync(string token, long userId, string sessionId)
    {
        var httpClient = _httpClientFactory.CreateClient(ProjectConsts.ACL_HTTP_CLIENT_NAME);
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _oAuthOption.AuthorizationConfigs.ApiToken);

        List<string> userAccessList = new();
        var offset = 0;
        var total = 0;
        do
        {
            var httpResult = await httpClient.GetAsync($"/srv/acl2/v2/acl/users/{userId}?size=50&offset={offset}&identityType=id&resourceId={_oAuthOption.AuthorizationConfigs.ResourceId}");
            if (httpResult.StatusCode == HttpStatusCode.OK)
            {
                var content = await httpResult.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<PodSsoGetUserAccessesResponse>(content);
                if (!response.hasError)
                {
                    foreach (var item in response.aclInfo)
                    {
                        if (item.role.active)
                            userAccessList.AddRange(item.role.rolePermissions.Select(x => x.name));
                    }
                }
            }
        } while (total > offset);
        return userAccessList;
    }
}
