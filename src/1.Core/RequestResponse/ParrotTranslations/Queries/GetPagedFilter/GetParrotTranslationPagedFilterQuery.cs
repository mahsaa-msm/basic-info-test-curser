using Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Queries.CommonResults;
using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Queries.GetPagedFilter;

public sealed class GetParrotTranslationPagedFilterQuery : PageQuery<PagedData<ParrotTranslationItemQr>>, IWebRequest
{
    public string? Key { get; set; }
    public string? Value { get; set; }
    public string? Culture { get; set; }

    public const string NamePath = "ParrotTranslation/GetParrotTranslationsPagedFilter";
    public string Path => $"/api/{NamePath}";
}
