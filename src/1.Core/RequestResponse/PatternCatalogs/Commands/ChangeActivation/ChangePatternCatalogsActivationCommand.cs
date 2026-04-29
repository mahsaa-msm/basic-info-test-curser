using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Commands.ChangeActivation;

public sealed class ChangePatternCatalogsActivationCommand : ICommand, IWebRequest
{
    public List<long> PatternCatalogsId { get; set; } = new();
    public bool IsActive { get; set; }

    public string Path => "/Api/PatternCatalog/ChangePatternCatalogsActivation";
}
