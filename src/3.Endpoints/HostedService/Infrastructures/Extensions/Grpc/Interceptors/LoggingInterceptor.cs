using Grpc.Core;
using Grpc.Core.Interceptors;
using System.Diagnostics;

namespace Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Extensions.Grpc.Interceptors;

public class LoggingInterceptor : Interceptor
{
    private readonly ILogger<LoggingInterceptor> _logger;

    public LoggingInterceptor(ILogger<LoggingInterceptor> logger)
    {
        _logger = logger;
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request,
                                                                                  ClientInterceptorContext<TRequest, TResponse> context,
                                                                                  AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        _logger.LogInformation("gRPC Client: Sending request to {Method}", context.Method.FullName);
        var stopwatch = Stopwatch.StartNew();

        var call = continuation(request, context);

        return new AsyncUnaryCall<TResponse>(HandleResponse(call.ResponseAsync,
                                                            context.Method.FullName,
                                                            stopwatch),
                                             call.ResponseHeadersAsync,
                                             call.GetStatus,
                                             call.GetTrailers,
                                             call.Dispose);
    }

    private async Task<TResponse> HandleResponse<TResponse>(Task<TResponse> task,
                                                            string methodName,
                                                            Stopwatch stopwatch)
    {
        try
        {
            var response = await task;
            stopwatch.Stop();
            _logger.LogInformation("gRPC Client: Received response from {Method} in {Elapsed}ms",
                                   methodName,
                                   stopwatch.ElapsedMilliseconds);
            return response;
        }
        catch (RpcException ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex,
                             "gRPC Client: Error in {Method} after {Elapsed}ms - Status: {Status}, Detail: {Detail}",
                             methodName,
                             stopwatch.ElapsedMilliseconds,
                             ex.Status.StatusCode,
                             ex.Status.Detail);
            throw;
        }
    }
}

