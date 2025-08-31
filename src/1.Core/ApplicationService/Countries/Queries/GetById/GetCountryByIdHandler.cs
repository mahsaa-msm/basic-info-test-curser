using Master.Data.Core.Contracts.Countries.Queries;
using Master.Data.Core.RequestResponse.Countries.Queries.GetById;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Countries.Queries.GetById;

public class GetCountryByIdHandler : QueryHandler<GetCountryByIdQuery, CountryQr>
{
    private readonly ICountryQueryRepository _queryRepository;

    public GetCountryByIdHandler(ZaminServices zaminServices,
                                 ICountryQueryRepository queryRepository)
        : base(zaminServices)
    {
        _queryRepository = queryRepository;
    }

    public override async Task<QueryResult<CountryQr>> Handle(GetCountryByIdQuery query)
        => Result(await _queryRepository.Execute(query));
}
