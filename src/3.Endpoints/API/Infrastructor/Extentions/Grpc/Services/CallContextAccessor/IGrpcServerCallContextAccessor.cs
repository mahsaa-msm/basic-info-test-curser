using Grpc.Core;

namespace Master.Data.Endpoints.API.Infrastructor.Extentions.Grpc.Services.CallContextAccessor;

public interface IGrpcServerCallContextAccessor
{
    ServerCallContext? ServerCallContext { get; set; }
}