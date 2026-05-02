using Vehicle.Insurance.Core.Domain.LicensePlateTypes.Parameters;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Commands.Create;

public sealed class CreateLicensePlateTypeCommand : ICommand<long>, IWebRequest
{
    public string Title { get; set; } = string.Empty;
    public string? DisplayTitle { get; set; }
    public string CoreId { get; set; } = string.Empty;

    public CreateLicensePlateTypeParameter ToCreateParameter(long priority) => new(Title,
        DisplayTitle,
        CoreId,
        priority);

    public string Path => "/Api/LicensePlateType/CreateLicensePlateType";
}
