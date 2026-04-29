using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.Countries.Commands.Delete;

public sealed class DeleteCountryCommand : ICommand, IWebRequest
{
    public long CountryId { get; set; }

    public string Path => "/Api/Country/DeleteCountry";
}
