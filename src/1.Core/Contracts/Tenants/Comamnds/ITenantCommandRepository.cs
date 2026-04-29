using Vehicle.Insurance.Core.Domain.Tenants.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace Vehicle.Insurance.Core.Contracts.Tenants.Comamnds;

public interface ITenantCommandRepository : ICommandRepository<Tenant, long>
{
    Task<List<Tenant>> GetByIds(List<long> tenantIds);
}

