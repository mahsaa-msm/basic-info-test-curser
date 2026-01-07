using Grpc.Core;
using Grpc.Core.Interceptors;
using Master.Data.Endpoints.API.Infrastructor.Extentions.Grpc.Services.CallContextAccessor;

namespace Master.Data.Endpoints.API.Infrastructor.Extentions.Grpc.Interceptors;

public sealed class GrpcContextMiddleware : Interceptor
{
    private readonly IGrpcServerCallContextAccessor _contextAccessor;

    public GrpcContextMiddleware(IGrpcServerCallContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request,
                                                                                  ServerCallContext context,
                                                                                  UnaryServerMethod<TRequest, TResponse> continuation)
    {
        _contextAccessor.ServerCallContext = context;

        try
        {
            return await continuation(request, context);
        }
        finally
        {
            _contextAccessor.ServerCallContext = null;
        }
    }
}