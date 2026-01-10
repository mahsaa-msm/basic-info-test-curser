using Master.Data.Core.Domain.Cities.Parameters;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.Cities.Commands.Create;
public sealed class CreateCityCommand : ICommand<long>, IWebRequest
{
    public string Title { get; set; } = string.Empty;
    public string? DisplayTitle { get; set; }
    public string Code { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public string ProvinceCoreId { get; set; } = string.Empty;

    public CreateCityParameter ToCreateParameter(long priority) => new(Title,
                                                                         DisplayTitle,
                                                                         CoreId,
                                                                         Code,
                                                                         priority,
                                                                         ProvinceCoreId);
    public RestoreCityParameter ToRestoreParameter(long priority) => new(Title,
                                                                           DisplayTitle,
                                                                           Code,
                                                                           priority,
                                                                           ProvinceCoreId);

    public string Path => "/Api/City/CreateCity";
}