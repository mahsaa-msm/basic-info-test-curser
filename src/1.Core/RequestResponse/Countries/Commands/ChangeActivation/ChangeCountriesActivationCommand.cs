using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.Countries.Commands.ChangeActivation;

public sealed class ChangeCountriesActivationCommand : ICommand, IWebRequest
{
    public List<long> CountriesId { get; set; } = new();
    public bool IsActive { get; set; }

    public string Path => "/Api/Country/ChangeCountriesActivation";
}
