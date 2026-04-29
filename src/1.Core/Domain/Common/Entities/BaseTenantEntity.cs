using Zamin.Core.Domain.Entities;
using Zamin.Core.Domain.ValueObjects;

namespace Vehicle.Insurance.Core.Domain.Common.Entities;

/// <summary>
/// The generic type of BaseTenantEntity<TId> has a bug in saveChange's interceptor.
/// please use BaseTenantEntity until fix this.
/// </summary>
public abstract class BaseTenantEntity : BaseTenantEntity<long>
{
}

/// <summary>
/// The generic type of BaseTenantEntity<TId> has a bug in saveChange's interceptor.
/// please use BaseTenantEntity until fix this.
/// </summary>
/// <typeparam name="TId"></typeparam>
public abstract class BaseTenantEntity<TId> : AggregateRoot<TId>
    where TId : struct, IComparable, IComparable<TId>, IConvertible, IEquatable<TId>, IFormattable
{
    public long TenantId { get; set; }
    public BusinessId? TenantBusinessId { get; set; }
}

