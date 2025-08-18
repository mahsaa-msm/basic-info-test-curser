using IdentityModel.Client;

namespace Master.Data.Endpoints.API.Infrastructor.Extentions.HttpClient.Services;


public class TokenService
{
    private readonly System.Net.Http.HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        bool ignoreSslCheck = Convert.ToBoolean(configuration["OAuth:IgnoreSslCheck"]);
        _httpClient = ignoreSslCheck ?
            new System.Net.Http.HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
            }) :
        new System.Net.Http.HttpClient();

        _configuration = configuration;
    }

    public async Task<string> GetTokenAsync()
    {
        var tokenEndpoint = _configuration["OAuth:TokenUrl"];
        var clientId = _configuration["OAuth:ClientId"];
        var clientSecret = _configuration["OAuth:Secret"];
        var scopes = string.Join(" ", _configuration.GetSection("OAuth:Scopes")
                                        .GetChildren()
                                        .Where(c => c.Key != "openid" && c.Key != "profile")
                                        .Select(x => x.Key));


        var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = tokenEndpoint,
            ClientId = clientId,
            ClientSecret = clientSecret,
            Scope = scopes
        });

        if (tokenResponse.IsError)
            throw new Exception($"Error requesting token: {tokenResponse.Error}");

        return tokenResponse.AccessToken;
    }
}