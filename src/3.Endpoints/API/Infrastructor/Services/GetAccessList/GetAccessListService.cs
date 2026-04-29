using Newtonsoft.Json;
using System.Net;

namespace Vehicle.Insurance.Endpoints.API.Infrastructor.Services.GetAccessList;

public class GetAccessListService : IGetAccessListService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public GetAccessListService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<AccessModel>?> ExecuteAsync()
    {
        //var token = await GetToken();
        var httpClient = new System.Net.Http.HttpClient();
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));
        //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var httpResult = await httpClient.GetAsync("http://localhost:4010/api/SoftwarePart/GetAccessList");
        if (httpResult.StatusCode == HttpStatusCode.OK)
            return GetDistinctList(await httpResult.Content.ReadAsStringAsync());

        return null;
    }

    private List<AccessModel> GetDistinctList(string content)
    {
        List<AccessModel> result = new();

        var accessList = JsonConvert.DeserializeObject<List<AccessModel>>(content);
        foreach (var accessItem in accessList)
        {
            if (!result.Any(x => x.NamePath == accessItem.NamePath))
                result.Add(accessItem);
        }

        return result;
    }
}

