namespace Master.Data.Endpoints.API.Infrastructor.Services.Tenant
{
    public interface ITenantResolver
    {
        long? ExtractTenantId(HttpContext context);
        Guid? ExtractTenantKey(HttpContext context);
    }
}
