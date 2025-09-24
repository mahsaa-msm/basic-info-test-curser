//using Master.Data.Core.Contracts.Common.Services;
//using Master.Data.Core.Resources;

//namespace Master.Data.Endpoints.API.Infrastructor.Middlewares;

//public class TenantMiddleware
//{
//    private readonly RequestDelegate _next;

//    public TenantMiddleware(RequestDelegate next)
//    {
//        _next = next;
//    }

//    public async Task Invoke(HttpContext context, ITenantService tenantService)
//    {
//        var tenantId = ExtractTenantId(context);
//        var tenantKey = ExtractTenantKey(context);

//        if (tenantId.HasValue || !string.IsNullOrEmpty(tenantKey))
//        {
//            Guid.TryParse(tenantKey, out var tenantKeyGuid);
//            tenantService.SetCurrentTenant(tenantId, tenantKeyGuid == Guid.Empty ?
//                                                        null :
//                                                        tenantKeyGuid);
//        }
//        else
//            tenantService.SetCurrentTenant(null, null);

//        await _next(context);
//    }

//    #region Methods
//    private long? ExtractTenantId(HttpContext context)
//    {
//        if (context.Request.Headers.TryGetValue(ProjectConsts.TENANT_ID_X_HEADER_NAME , out var tenantIdHeader) &&
//            long.TryParse(tenantIdHeader, out var tenantId))
//            return tenantId;

//        if (context.Request.Query.TryGetValue(ProjectConsts.TENANT_ID_HEADER_NAME , out var tenantIdQuery) &&
//            long.TryParse(tenantIdQuery, out tenantId))
//            return tenantId;

//        return null;
//    }

//    private string? ExtractTenantKey(HttpContext context)
//    {
//        if (context.Request.Headers.TryGetValue(ProjectConsts.TENANT_KEY_X_HEADER_NAME , out var tenantKeyHeader))
//            return tenantKeyHeader;

//        if (context.Request.Query.TryGetValue(ProjectConsts.TENANT_KEY_HEADER_NAME, out var tenantKeyQuery))
//            return tenantKeyQuery;

//        return null;
//    }
//    #endregion
//}
