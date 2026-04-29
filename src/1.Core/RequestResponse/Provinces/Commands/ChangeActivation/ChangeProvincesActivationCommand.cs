using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.Provinces.Commands.ChangeActivation;

public sealed class ChangeProvincesActivationCommand : ICommand, IWebRequest
{
    public List<long> ProvincesId { get; set; } = new();
    public bool IsActive { get; set; }

    public string Path => "/Api/Province/ChangeProvincesActivation";
}
