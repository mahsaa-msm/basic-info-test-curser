using Vehicle.Insurance.Core.Contracts.Tenants.Queries;
using Vehicle.Insurance.Core.RequestResponse.Tenants.Queries.GetById;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.Tenants.Queries;

public sealed class GetTenantByIdHandler : QueryHandler<GetTenantByIdQuery, TenantGraphQr?>
{
    private readonly ITenantQueryRepository _tenantQueryRepository;

    public GetTenantByIdHandler(ZaminServices zaminServices,
                                ITenantQueryRepository tenantQueryRepository)
        : base(zaminServices)
    {
        _tenantQueryRepository = tenantQueryRepository;
    }

    public override async Task<QueryResult<TenantGraphQr?>> Handle(GetTenantByIdQuery query)
        => await ResultAsync(await _tenantQueryRepository.Execute(query));
}

