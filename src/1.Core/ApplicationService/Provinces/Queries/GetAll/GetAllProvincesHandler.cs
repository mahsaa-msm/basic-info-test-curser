using Master.Data.Core.Contracts.Provinces.Queries;
using Master.Data.Core.RequestResponse.Provinces.Queries.GetAll;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Provinces.Queries.GetAll;

public class GetAllProvincesHandler : QueryHandler<GetAllProvincesQuery, List<ProvinceSelectItemQr>>
{
    private readonly IProvinceQueryRepository _provinceQueryRepository;

    public GetAllProvincesHandler(ZaminServices zaminServices,
                                  IProvinceQueryRepository provinceQueryRepository)
        : base(zaminServices)
    {
        _provinceQueryRepository = provinceQueryRepository;
    }

    public override async Task<QueryResult<List<ProvinceSelectItemQr>>> Handle(GetAllProvincesQuery query)
        => Result(await _provinceQueryRepository.Execute(query));
}
