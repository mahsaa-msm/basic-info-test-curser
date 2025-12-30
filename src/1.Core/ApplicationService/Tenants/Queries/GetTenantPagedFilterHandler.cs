using Master.Data.Core.Contracts.Tenants.Queries;
using Master.Data.Core.RequestResponse.Tenants.Queries.GetPagedFilter;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Tenants.Queries;
public sealed class GetTenantPagedFilterHandler : QueryHandler<GetTenantPagedFilterQuery, PagedData<TenantSelectItemQr>>
{
    private readonly ITenantQueryRepository _tenantQueryRepository;

    public GetTenantPagedFilterHandler(ZaminServices zaminServices,
                                       ITenantQueryRepository tenantQueryRepository)
        : base(zaminServices)
    {
        _tenantQueryRepository = tenantQueryRepository;
    }

    public override async Task<QueryResult<PagedData<TenantSelectItemQr>>> Handle(GetTenantPagedFilterQuery query)
        => await ResultAsync(await _tenantQueryRepository.ExecuteAsync(query));
}
