using Vehicle.Insurance.Core.Contracts.LicensePlateTypes.Queries;
using Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Queries.GetById;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.LicensePlateTypes.Queries.GetById;

public sealed class GetLicensePlateTypeByIdHandler : QueryHandler<GetLicensePlateTypeByIdQuery, LicensePlateTypeQr?>
{
    private readonly ILicensePlateTypeQueryRepository _queryRepository;

    public GetLicensePlateTypeByIdHandler(ZaminServices zaminServices,
        ILicensePlateTypeQueryRepository queryRepository) : base(zaminServices)
    {
        _queryRepository = queryRepository;
    }

    public override async Task<QueryResult<LicensePlateTypeQr?>> Handle(GetLicensePlateTypeByIdQuery query)
        => Result(await _queryRepository.Execute(query));
}
