using Grpc.Core;
using Grpc.Core.Interceptors;
using Master.Data.Core.Contracts.Common.Options;

namespace Master.Data.Endpoints.API.Infrastructor.Extentions.Grpc.Interceptors;

public class ApiKeyServerInterceptor : Interceptor
{
    private readonly ILogger<ApiKeyServerInterceptor> _logger;
    private readonly GrpcOption _grpcOption;

    public ApiKeyServerInterceptor(ILogger<ApiKeyServerInterceptor> logger,
                                   GrpcOption grpcOption)
    {
        _logger = logger;
        _grpcOption = grpcOption;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        // اعتبارسنجی API Key دریافتی
        var apiKey = context.RequestHeaders
            .FirstOrDefault(header => header.Key == _grpcOption.ApiKeyName)?.Value;
        
        if (string.IsNullOrEmpty(apiKey) || apiKey != _grpcOption.ApiKey)
        {
            throw new RpcException(new Status(StatusCode.Unauthenticated,
                                              "Invalid API Key"));
        }

        return await continuation(request, context);
    }
}