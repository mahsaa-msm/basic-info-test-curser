using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.ParrotTranslations.Commands.Delete;

public sealed class DeleteParrotTranslationCommand : ICommand, IWebRequest
{
    public long Id { get; set; }

    public const string NamePath = "ParrotTranslation/DeleteParrotTranslation";
    public string Path => $"/api/{NamePath}";

}
