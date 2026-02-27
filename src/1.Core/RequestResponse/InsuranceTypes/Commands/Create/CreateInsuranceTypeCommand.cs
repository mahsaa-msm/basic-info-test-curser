using Master.Data.Core.Domain.InsuranceTypes.Parameters;
using Master.Data.Core.Resources;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.InsuranceTypes.Commands.Create;

public sealed class CreateInsuranceTypeCommand : ICommand<long>, IWebRequest
{
    public string Title { get; set; } = string.Empty;
    public string? DisplayTitle { get; set; }
    public string Code { get; set; } = string.Empty;
    public ServiceFeatureCategory? ServiceFeatureCategory { get; set; } 
    public string CoreId { get; set; } = string.Empty;

    public CreateInsuranceTypeParameter ToCreateParameter(long priority) => new(Title,
                                                                         DisplayTitle,
                                                                         CoreId,
                                                                         Code,
                                                                         ServiceFeatureCategory,
                                                                         priority);
    public RestoreInsuranceTypeParameter ToRestoreParameter(long priority) => new(Title,
                                                                           DisplayTitle,
                                                                           Code,
                                                                           ServiceFeatureCategory,
                                                                           priority);

    public string Path => "/Api/InsuranceType/CreateInsuranceType";
}