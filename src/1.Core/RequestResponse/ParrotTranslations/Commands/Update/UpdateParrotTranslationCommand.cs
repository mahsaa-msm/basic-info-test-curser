using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.ParrotTranslations.Commands.Update;

public sealed class UpdateParrotTranslationCommand : ICommand, IWebRequest
{
    public long Id { get; set; }
    public string Key { get; set; } = default!;
    public string Value { get; set; } = default!;
    public string? Culture { get; set; }

    public const string NamePath = "ParrotTranslation/UpdateParrotTranslation";
    public string Path => $"/api/{NamePath}";
}
