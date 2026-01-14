using Master.Data.Core.Contracts.Tenants.Queries;
using Master.Data.Core.RequestResponse.Tenants.Queries.GetIAllSelectItem;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Tenants.Queries;

public sealed class GetAllTenantsHandler : QueryHandler<GetAllTenantsSelectItemQuery, List<TenantIdKeyQr>>
{
    private readonly ITenantQueryRepository _tenantQueryRepository;

    public GetAllTenantsHandler(ZaminServices zaminServices,
                                ITenantQueryRepository tenantQueryRepository)
        : base(zaminServices)
    {
        _tenantQueryRepository = tenantQueryRepository;
    }

    public override async Task<QueryResult<List<TenantIdKeyQr>>> Handle(GetAllTenantsSelectItemQuery query)
        => await ResultAsync(await _tenantQueryRepository.ExecuteAsync(query));
}

