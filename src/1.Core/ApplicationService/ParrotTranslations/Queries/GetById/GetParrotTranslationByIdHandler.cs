using Master.Data.Core.Contracts.ParrotTranslations.Queries;
using Master.Data.Core.RequestResponse.ParrotTranslations.Queries.CommonResults;
using Master.Data.Core.RequestResponse.ParrotTranslations.Queries.GetById;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.ParrotTranslations.Queries.GetById;

public sealed class GetParrotTranslationByIdHandler : QueryHandler<GetParrotTranslationByIdQuery, ParrotTranslationItemQr?>
{
    private readonly IParrotTranslationQueryRepository _parrotTranslationQueryRepository;

    public GetParrotTranslationByIdHandler(ZaminServices zaminServices,
                                           IParrotTranslationQueryRepository parrotTranslationQueryRepository) 
        : base(zaminServices)
    {
        _parrotTranslationQueryRepository = parrotTranslationQueryRepository;
    }

    public override async Task<QueryResult<ParrotTranslationItemQr?>> Handle(GetParrotTranslationByIdQuery query)
        => await ResultAsync(await _parrotTranslationQueryRepository.Execute(query));
}