using Master.Data.Core.Contracts.ParrotTranslations.Queries;
using Master.Data.Core.RequestResponse.ParrotTranslations.Queries.CommonResults;
using Master.Data.Core.RequestResponse.ParrotTranslations.Queries.GetAll;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.ParrotTranslations.Queries.GetAll;

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