using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Commands.ChangeActivation;

public sealed class ChangeLicensePlateTypesActivationCommand : ICommand, IWebRequest
{
    public List<long> LicensePlateTypesId { get; set; } = new();
    public bool IsActive { get; set; }

    public string Path => "/Api/LicensePlateType/ChangeLicensePlateTypesActivation";
}
