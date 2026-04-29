using Vehicle.Insurance.Core.Domain.Cities.Parameters;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.Cities.Commands.Update;

public sealed class UpdateCityCommand : ICommand, IWebRequest
{
    public long CityId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public long Priority { get; set; }
    public string ProvinceCoreId { get; set; } = string.Empty;

    public UpdateCityParameter ToParameter() => new(Title,
                                                       DisplayTitle,
                                                       Code,
                                                       Priority,
                                                       ProvinceCoreId);

    public string Path => "/Api/City/UpdateCity";
}

