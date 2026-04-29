using Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Queries.CommonResults;
using Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Queries.GetById;
using Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Queries.GetPagedFilter;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.Contracts.ParrotTranslations.Queries;

public interface IParrotTranslationQueryRepository : IQueryRepository
{
    public Task<ParrotTranslationItemQr?> Execute(GetParrotTranslationByIdQuery query);

    public Task<PagedData<ParrotTranslationItemQr>> ExecuteAsync(GetParrotTranslationPagedFilterQuery query);

    public Task<ParrotTranslationQr> ExecuteAsync(GetAllParrotTranslationQuery query);
}

