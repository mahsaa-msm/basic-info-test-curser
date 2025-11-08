using Master.Data.Core.Domain.Provinces.Parameters;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.Provinces.Commands.Create;
public sealed class CreateProvinceCommand : ICommand<long>, IWebRequest
{
    public string Title { get; set; } = string.Empty;
    public string? DisplayTitle { get; set; }
    public string Code { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public string CountryCoreId { get; set; } = string.Empty;

    public CreateProvinceParameter ToCreateParameter(long priority) => new(Title,
                                                                         DisplayTitle,
                                                                         CoreId,
                                                                         Code,
                                                                         priority,
                                                                         CountryCoreId);
    public RestoreProvinceParameter ToRestoreParameter(long priority) => new(Title,
                                                                           DisplayTitle,
                                                                           Code,
                                                                           priority,
                                                                           CountryCoreId);

    public string Path => "/Api/Province/CreateProvince";
}