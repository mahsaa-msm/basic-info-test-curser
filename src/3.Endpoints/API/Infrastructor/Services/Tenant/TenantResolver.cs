using Grpc.Core;
using Master.Data.Core.Resources;
using Master.Data.Endpoints.API.Infrastructor.Extentions.Grpc.Services.CallContextAccessor;

namespace Master.Data.Endpoints.API.Infrastructor.Services.Tenant;

public class TenantResolver : ITenantResolver
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IGrpcServerCallContextAccessor _grpcContextAccessor;

    public TenantResolver(IHttpContextAccessor httpContextAccessor,
                          IGrpcServerCallContextAccessor grpcContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _grpcContextAccessor = grpcContextAccessor;
    }

    public long? ExtractTenantId()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext != null)
        {
            return ExtractTenantIdHttp(httpContext);
        }

        var grpcContext = _grpcContextAccessor.ServerCallContext;
        if (grpcContext != null)
        {
            return ExtractTenantIdGrpc(grpcContext);
        }

        return null;
    }

    public long? ExtractTenantIdGrpc(ServerCallContext serverCallContext)
    {
        var tenantIdHeader = serverCallContext.RequestHeaders.FirstOrDefault(h =>
             h.Key.Equals(ProjectConsts.TENANT_ID_X_HEADER_NAME, StringComparison.OrdinalIgnoreCase) ||
             h.Key.Equals(ProjectConsts.TENANT_ID_HEADER_NAME, StringComparison.OrdinalIgnoreCase));

        if (tenantIdHeader != null && long.TryParse(tenantIdHeader.Value, out var tenantId))
        {
            return tenantId;
        }

        return null;
    }

    public long? ExtractTenantIdHttp(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue(ProjectConsts.TENANT_ID_X_HEADER_NAME, out var tenantIdHeader)
            && long.TryParse(tenantIdHeader, out var tenantId))
        {
            return tenantId;
        }

        if (context.Request.Query.TryGetValue(ProjectConsts.TENANT_ID_HEADER_NAME, out var tenantIdQuery)
            && long.TryParse(tenantIdQuery, out tenantId))
        {
            return tenantId;
        }

        return null;
    }

    public Guid? ExtractTenantKey()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext != null)
        {
            return ExtractTenantKeyHttp(httpContext);
        }

        var grpcContext = _grpcContextAccessor.ServerCallContext;
        if (grpcContext != null)
        {
            return ExtractTenantKeyGrpc(grpcContext);
        }

        return null;
    }

    public Guid? ExtractTenantKeyGrpc(ServerCallContext serverCallContext)
    {
        var tenantKeyHeader = serverCallContext.RequestHeaders.FirstOrDefault(h =>
            h.Key.Equals(ProjectConsts.TENANT_KEY_X_HEADER_NAME, StringComparison.OrdinalIgnoreCase) ||
            h.Key.Equals(ProjectConsts.TENANT_KEY_HEADER_NAME, StringComparison.OrdinalIgnoreCase));

        if (tenantKeyHeader != null && Guid.TryParse(tenantKeyHeader.Value, out var tenantKey))
        {
            return tenantKey;
        }

        return null;
    }

    public Guid? ExtractTenantKeyHttp(HttpContext context)
    {
        Guid tenantKey;

        if (context.Request.Headers.TryGetValue(ProjectConsts.TENANT_KEY_X_HEADER_NAME, out var tenantKeyHeader)
            && Guid.TryParse(tenantKeyHeader, out tenantKey))
        {
            return tenantKey;
        }

        if (context.Request.Query.TryGetValue(ProjectConsts.TENANT_KEY_HEADER_NAME, out var tenantKeyQuery)
            && Guid.TryParse(tenantKeyQuery, out tenantKey))
        {
            return tenantKey;
        }

        return null;
    }
}

