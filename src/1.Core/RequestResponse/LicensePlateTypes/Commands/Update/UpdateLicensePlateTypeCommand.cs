using Vehicle.Insurance.Core.Domain.LicensePlateTypes.Parameters;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Commands.Update;

public sealed class UpdateLicensePlateTypeCommand : ICommand, IWebRequest
{
    public long LicensePlateTypeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public long Priority { get; set; }

    public UpdateLicensePlateTypeParameter ToParameter() => new(Title,
        DisplayTitle,
        Priority);

    public string Path => "/Api/LicensePlateType/UpdateLicensePlateType";
}
