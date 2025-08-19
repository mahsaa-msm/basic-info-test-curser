using Master.Data.Core.Contracts.Common.Services;

namespace Master.Data.Endpoints.API.Infrastructor.Middlewares;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ITenantService tenantService)
    {
        var tenantId = ExtractTenantId(context);
        var tenantKey = ExtractTenantKey(context);

        if (tenantId.HasValue || !string.IsNullOrEmpty(tenantKey))
        {
            Guid.TryParse(tenantKey, out var tenantKeyGuid);
            tenantService.SetCurrentTenant(tenantId, tenantKeyGuid == Guid.Empty ?
                                                        null :
                                                        tenantKeyGuid);
        }
        else
            tenantService.SetCurrentTenant(null, null);

        await _next(context);
    }

    #region Methods
    private long? ExtractTenantId(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("X-Tenant-Id", out var tenantIdHeader) &&
            long.TryParse(tenantIdHeader, out var tenantId))
            return tenantId;

        if (context.Request.Query.TryGetValue("tenantId", out var tenantIdQuery) &&
            long.TryParse(tenantIdQuery, out tenantId))
            return tenantId;

        return null;
    }

    private string? ExtractTenantKey(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("X-Tenant-Key", out var tenantKeyHeader))
            return tenantKeyHeader;

        if (context.Request.Query.TryGetValue("tenantKey", out var tenantKeyQuery))
            return tenantKeyQuery;

        return null;
    }
    #endregion
}
