using Vehicle.Insurance.Core.Contracts.ParrotTranslations.Queries;
using Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Queries.CommonResults;
using Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Queries.GetAll;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.ParrotTranslations.Queries.GetAll;

public sealed class GetAllParrotTranslationHandler : QueryHandler<GetAllParrotTranslationQuery, ParrotTranslationQr>
{
    private readonly IParrotTranslationQueryRepository _parrotTranslationQueryRepository;

    public GetAllParrotTranslationHandler(ZaminServices zaminServices,
                                          IParrotTranslationQueryRepository parrotTranslationQueryRepository)
        : base(zaminServices)
    {
        _parrotTranslationQueryRepository = parrotTranslationQueryRepository;
    }

    public override async Task<QueryResult<ParrotTranslationQr>> Handle(GetAllParrotTranslationQuery query)
        => await ResultAsync(await _parrotTranslationQueryRepository.ExecuteAsync(query));
}
