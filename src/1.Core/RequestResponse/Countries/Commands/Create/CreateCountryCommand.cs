using Master.Data.Core.Domain.Countries.Parameters;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.Countries.Commands.Create;

public sealed class CreateCountryCommand : ICommand<long>, IWebRequest
{
    public string Title { get; set; } = string.Empty;
    public string? DisplayTitle { get; set; }
    public string Code { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;

    public CreateCountryParameter ToCreateParameter(int priority) => new(Title,
                                                                         DisplayTitle,
                                                                         CoreId,
                                                                         Code,
                                                                         priority);
    public RestoreCountryParameter ToRestoreParameter(int priority) => new(Title,
                                                                           DisplayTitle,
                                                                           Code,
                                                                           priority);

    public string Path => "/Api/Country/CreateCountry";
}