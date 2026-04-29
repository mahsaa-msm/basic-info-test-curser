using System.Net;

namespace Vehicle.Insurance.Endpoints.API.Infrastructor.Extentions.HttpClient.Handlers;

public class ExceptionHandler : DelegatingHandler
{
    private readonly ILogger<ExceptionHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ExceptionHandler(ILogger<ExceptionHandler> logger,
                            IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                 CancellationToken cancellationToken)
    {
        HttpResponseMessage response = null;
        var context = _httpContextAccessor.HttpContext;
        try
        {
            response = await base.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Api call: {Request}, returned unsuccessful status code: {StatusCode}",
                                 request,
                                 response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            response = CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);

            if (context != null)
            {
                response.Headers.Add("RequestId", context.TraceIdentifier);
                _logger.LogError("Api call: {Request}, with requestId: {RequestId}, has exception: {Exception}",
                                 request,
                                 context.TraceIdentifier,
                                 ex);
            }
        }
        return response;
    }

    private HttpResponseMessage CreateErrorResponse(HttpStatusCode statusCode, string message)
    {
        var response = new HttpResponseMessage(statusCode)
        {
            Content = new StringContent($"An external service error occurred: {message}")
        };
        return response;
    }
}

