using Vehicle.Insurance.Core.Contracts.ParrotTranslations.Queries;
using Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Queries.CommonResults;
using Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Queries.GetPagedFilter;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.ParrotTranslations.Queries.GetPagedFilter;

public sealed class GetParrotTranslationPagedFilterHandler : QueryHandler<GetParrotTranslationPagedFilterQuery, PagedData<ParrotTranslationItemQr>>
{
    private readonly IParrotTranslationQueryRepository _parrotTranslationQueryRepository;

    public GetParrotTranslationPagedFilterHandler(ZaminServices zaminServices,
                                                  IParrotTranslationQueryRepository parrotTranslationQueryRepository)
        : base(zaminServices)
    {
        _parrotTranslationQueryRepository = parrotTranslationQueryRepository;
    }

    public override async Task<QueryResult<PagedData<ParrotTranslationItemQr>>> Handle(GetParrotTranslationPagedFilterQuery query)
        => await ResultAsync(await _parrotTranslationQueryRepository.ExecuteAsync(query));
}
