using Grpc.Core;

namespace Vehicle.Insurance.Endpoints.API.Infrastructor.Extentions.Grpc.Services.CallContextAccessor;

public interface IGrpcServerCallContextAccessor
{
    ServerCallContext? ServerCallContext { get; set; }
}
