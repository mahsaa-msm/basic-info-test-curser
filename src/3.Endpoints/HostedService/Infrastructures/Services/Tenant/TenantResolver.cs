using Vehicle.Insurance.Core.Resources;
using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.Tenant;

public class TenantResolver : ITenantResolver, ITransientLifetime
{
    public long? ExtractTenantId(HttpContext context)
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

    public Guid? ExtractTenantKey(HttpContext context)
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


