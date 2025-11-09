using Master.Data.Core.RequestResponse.ParrotTranslations.Queries.CommonResults;
using Master.Data.Core.RequestResponse.ParrotTranslations.Queries.GetAll;
using Master.Data.Core.RequestResponse.ParrotTranslations.Queries.GetById;
using Master.Data.Core.RequestResponse.ParrotTranslations.Queries.GetPagedFilter;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.Contracts.ParrotTranslations.Queries;

public interface IParrotTranslationQueryRepository : IQueryRepository
{
    public Task<ParrotTranslationItemQr?> Execute(GetParrotTranslationByIdQuery query);

    public Task<PagedData<ParrotTranslationItemQr>> ExecuteAsync(GetParrotTranslationPagedFilterQuery query);

    public Task<ParrotTranslationQr> ExecuteAsync(GetAllParrotTranslationQuery query);
}
