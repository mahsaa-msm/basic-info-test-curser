using Master.Data.Core.Contracts.Common.Services.Tenant;
using Master.Data.Core.Resources;
using Microsoft.AspNetCore.Http;

namespace Master.Data.Endpoints.API.Infrastructor.Services.Tenant;

public class TenantResolver : ITenantResolver
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

