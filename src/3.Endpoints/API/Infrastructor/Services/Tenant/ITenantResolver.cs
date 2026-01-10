using Grpc.Core;

namespace Master.Data.Endpoints.API.Infrastructor.Services.Tenant;

public interface ITenantResolver
{
    long? ExtractTenantIdHttp(HttpContext context);
    Guid? ExtractTenantKeyHttp(HttpContext context);

    long? ExtractTenantIdGrpc(ServerCallContext serverCallContext);
    Guid? ExtractTenantKeyGrpc(ServerCallContext serverCallContext);

    long? ExtractTenantId();
    Guid? ExtractTenantKey();
}
