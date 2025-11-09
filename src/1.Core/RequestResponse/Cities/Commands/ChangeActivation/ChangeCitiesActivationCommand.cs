using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.Cities.Commands.ChangeActivation;
public sealed class ChangeCitiesActivationCommand : ICommand, IWebRequest
{
    public List<long> CitiesId { get; set; } = new();
    public bool IsActive { get; set; }

    public string Path => "/Api/City/ChangeCitiesActivation";
}