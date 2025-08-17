using Grpc.Core;
using Grpc.Core.Interceptors;
using Master.Data.Core.Contracts.Common.Options;

namespace Master.Data.Endpoints.API.Infrastructor.Grpc.Interceptors;

public class ApiKeyInterceptor : Interceptor
{
    private readonly GrpcOption _grpcOption;
    private readonly ILogger<ApiKeyInterceptor> _logger;
    private readonly string _serverName;

    public ApiKeyInterceptor(GrpcOption grpcOption,
                             ILogger<ApiKeyInterceptor> logger,
                             string serverName)
    {
        _grpcOption = grpcOption;
        _logger = logger;
        _serverName = serverName;
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request,
                                                                                  ClientInterceptorContext<TRequest, TResponse> context,
                                                                                  AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        var serverConfig = _grpcOption.GrpcServers.FirstOrDefault(s => s.ServerName == _serverName);

        if (serverConfig == null)
        {
            _logger.LogError("gRPC Client: Server configuration not found for {ServerName}", _serverName);
            throw new InvalidOperationException($"Server configuration not found for {_serverName}");
        }

        if (serverConfig.NeedAuth && !string.IsNullOrEmpty(serverConfig.ApiKey))
        {
            var metadata = new Metadata
            {
                { serverConfig.ApiKeyName, serverConfig.ApiKey }
            };

            var newContext = new ClientInterceptorContext<TRequest, TResponse>(context.Method,
                                                                               context.Host,
                                                                               context.Options.WithHeaders(metadata));

            _logger.LogDebug("gRPC Client: Added API key for {ServerName}", _serverName);
            return continuation(request, newContext);
        }

        return continuation(request, context);
    }
}
