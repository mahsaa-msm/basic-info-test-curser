using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Vehicle.Insurance.Endpoints.API.Infrastructor.Extentions.Grpc.Interceptors;

public class ExceptionInterceptor : Interceptor
{
    private readonly ILogger<ExceptionInterceptor> _logger;

    public ExceptionInterceptor(ILogger<ExceptionInterceptor> logger)
    {
        _logger = logger;
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request,
                                                                                  ClientInterceptorContext<TRequest, TResponse> context,
                                                                                  AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        try
        {
            return base.AsyncUnaryCall(request, context, continuation);
        }
        catch (RpcException ex)
        {
            _logger.LogError("gRPC Client: Exception interceptor caught error: {Status}", ex.Status);

            // تبدیل به استثنای دامنه
            throw ex.StatusCode switch
            {
                StatusCode.PermissionDenied => new UnauthorizedAccessException("Access denied", ex),
                StatusCode.DeadlineExceeded => new TimeoutException("Request timed out", ex),
                _ => new ApplicationException("gRPC communication failed", ex)
            };
        }
    }
}

