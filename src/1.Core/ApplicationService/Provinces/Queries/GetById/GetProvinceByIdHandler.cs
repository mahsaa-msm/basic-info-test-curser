using Vehicle.Insurance.Core.Contracts.Provinces.Queries;
using Vehicle.Insurance.Core.RequestResponse.Provinces.Queries.GetById;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.Provinces.Queries.GetById;

public sealed class GetProvinceByIdHandler : QueryHandler<GetProvinceByIdQuery, ProvinceQr>
{
    private readonly IProvinceQueryRepository _queryRepository;

    public GetProvinceByIdHandler(ZaminServices zaminServices,
                                 IProvinceQueryRepository queryRepository)
        : base(zaminServices)
    {
        _queryRepository = queryRepository;
    }

    public override async Task<QueryResult<ProvinceQr>> Handle(GetProvinceByIdQuery query)
        => Result(await _queryRepository.Execute(query));
}

