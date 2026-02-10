using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.IssuanceSchemes.Commands.ChangeActivation;

public sealed class ChangeIssuanceSchemesActivationCommand : ICommand, IWebRequest
{
    public List<long> IssuanceSchemesId { get; set; } = new();
    public bool IsActive { get; set; }

    public string Path => "/Api/IssuanceScheme/ChangeIssuanceSchemesActivation";
}