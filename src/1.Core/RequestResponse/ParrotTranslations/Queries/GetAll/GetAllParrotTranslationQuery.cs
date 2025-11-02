using Master.Data.Core.RequestResponse.ParrotTranslations.Queries.CommonResults;
using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.ParrotTranslations.Queries.GetAll;

public sealed class GetAllParrotTranslationQuery : IQuery<ParrotTranslationQr>, IWebRequest
{
    public const string NamePath = "ParrotTranslation/GetAllParrotTranslations";
    public string Path => $"/api/{NamePath}";
}