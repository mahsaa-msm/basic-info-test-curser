namespace Master.Data.Endpoints.HostedService.Infrastructures.Services.Tenant
{
    public interface ITenantResolver
    {
        long? ExtractTenantId(HttpContext context);
        Guid? ExtractTenantKey(HttpContext context);
    }
}
