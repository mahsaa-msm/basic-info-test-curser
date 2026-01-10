using Master.Data.Core.RequestResponse.ParrotTranslations.Queries.CommonResults;
using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.ParrotTranslations.Queries.GetById;

public sealed class GetParrotTranslationByIdQuery : IQuery<ParrotTranslationItemQr?>, IWebRequest
{
    public long Id { get; set; }

    public const string NamePath = "ParrotTranslation/GetParrotTranslationById";
    public string Path => $"/api/{NamePath}";
}