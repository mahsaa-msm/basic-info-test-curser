using Vehicle.Insurance.Core.Domain.InsuranceTypes.Parameters;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.InsuranceTypes.Commands.Update;

public sealed class UpdateInsuranceTypeCommand : ICommand, IWebRequest
{
    public long InsuranceTypeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public ServiceFeatureCategory? ServiceFeatureCategory { get; set; }
    public long Priority { get; set; }

    public UpdateInsuranceTypeParameter ToParameter() => new(Title,
                                                             DisplayTitle,
                                                             Code,
                                                             ServiceFeatureCategory,
                                                             Priority);

    public string Path => "/Api/InsuranceType/UpdateInsuranceType";
}
