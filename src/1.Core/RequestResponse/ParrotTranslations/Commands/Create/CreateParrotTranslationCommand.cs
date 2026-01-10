using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.ParrotTranslations.Commands.Create;

public sealed class CreateParrotTranslationCommand : ICommand, IWebRequest
{
    public string Key { get; set; } = default!;
    public string Value { get; set; } = default!;
    public string? Culture { get; set; }

    public const string NamePath = "ParrotTranslation/CreateParrotTranslation";
    public string Path => $"/api/{NamePath}";

}