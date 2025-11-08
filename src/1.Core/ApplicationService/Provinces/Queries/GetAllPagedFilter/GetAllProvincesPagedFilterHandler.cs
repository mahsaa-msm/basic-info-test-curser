using Master.Data.Core.Contracts.Provinces.Queries;
using Master.Data.Core.RequestResponse.Provinces.Queries.GetAllPagedFilter;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Provinces.Queries.GetAllPagedFilter;

public class GetAllProvincesPagedFilterHandler : QueryHandler<GetAllProvincesPagedFilterQuery, PagedData<ProvinceListItemQr>>
{
    private readonly IProvinceQueryRepository _queryRepository;

    public GetAllProvincesPagedFilterHandler(ZaminServices zaminServices,
                                             IProvinceQueryRepository queryRepository)
        : base(zaminServices)
    {
        _queryRepository = queryRepository;
    }

    public override async Task<QueryResult<PagedData<ProvinceListItemQr>>> Handle(GetAllProvincesPagedFilterQuery query)
        => Result(await _queryRepository.Execute(query));
}