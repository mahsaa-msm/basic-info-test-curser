using Master.Data.Core.Domain.InsuranceTypes.Parameters;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.InsuranceTypes.Commands.Update;

public sealed class UpdateInsuranceTypeCommand : ICommand, IWebRequest
{
    public long InsuranceTypeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public long Priority { get; set; }

    public UpdateInsuranceTypeParameter ToParameter() => new(Title,
                                                       DisplayTitle,
                                                       Code,
                                                       Priority);

    public string Path => "/Api/InsuranceType/UpdateInsuranceType";
}