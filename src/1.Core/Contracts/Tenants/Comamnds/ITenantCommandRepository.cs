using Master.Data.Core.Domain.Tenants.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace Master.Data.Core.Contracts.Tenants.Comamnds;

public interface ITenantCommandRepository : ICommandRepository<Tenant, long>
{
    Task<List<Tenant>> GetByIds(List<long> tenantIds);
}
