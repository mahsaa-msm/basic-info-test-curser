using Grpc.Core;

namespace Vehicle.Insurance.Endpoints.API.Infrastructor.Extentions.Grpc.Services.CallContextAccessor;

public sealed class GrpcServerCallContextAccessor : IGrpcServerCallContextAccessor
{
    private static readonly AsyncLocal<ServerCallContext?> _currentContext = new();

    public ServerCallContext? ServerCallContext
    {
        get => _currentContext.Value;
        set => _currentContext.Value = value;
    }
}
