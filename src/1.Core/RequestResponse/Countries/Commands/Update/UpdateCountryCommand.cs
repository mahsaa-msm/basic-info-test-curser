using Vehicle.Insurance.Core.Domain.Countries.Parameters;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.Countries.Commands.Update;

public sealed class UpdateCountryCommand : ICommand, IWebRequest
{
    public long CountryId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public long Priority { get; set; }

    public UpdateCountryParameter ToParameter() => new(Title,
                                                       DisplayTitle,
                                                       Code,
                                                       Priority);

    public string Path => "/Api/Country/UpdateCountry";
}
