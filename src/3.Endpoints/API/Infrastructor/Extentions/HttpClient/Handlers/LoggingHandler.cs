using Newtonsoft.Json;

namespace Vehicle.Insurance.Endpoints.API.Infrastructor.Extentions.HttpClient.Handlers;

public class LoggingHandler(ILogger<LoggingHandler> logger) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                 CancellationToken cancellationToken)
    {
        var serializerSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore // نادیده گرفتن حلقه‌ها
        };

        logger.LogInformation("Sending request to {Url} with {Request}",
                              request.RequestUri,
                              JsonConvert.SerializeObject(request, serializerSettings));

        var response = await base.SendAsync(request, cancellationToken);

        logger.LogInformation("Received response from {Url} with {Response}",
                              request.RequestUri,
                              JsonConvert.SerializeObject(response, serializerSettings));

        return response;
    }
}

