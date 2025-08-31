namespace Master.Data.Endpoints.API.Infrastructor.DependencyInjection.DbContext.CacheKeyFactory;

internal class TenantModelCacheKey
{
    private readonly Type _contextType;
    private readonly long? _tenantId;
    private readonly Guid? _tenantKey;

    public TenantModelCacheKey(Type type, long? tenantId, Guid? tenantKey)
    {
        _contextType = type;
        _tenantId = tenantId;
        _tenantKey = tenantKey;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not TenantModelCacheKey other)
            return false;

        return _contextType == other._contextType &&
               _tenantId == other._tenantId &&
               _tenantKey == other._tenantKey;
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(_contextType);
        hash.Add(_tenantId);
        hash.Add(_tenantKey);
        return hash.ToHashCode();
    }
}