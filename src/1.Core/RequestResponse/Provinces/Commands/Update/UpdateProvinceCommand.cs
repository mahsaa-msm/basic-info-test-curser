using Master.Data.Core.Domain.Provinces.Parameters;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.Provinces.Commands.Update;
public sealed class UpdateProvinceCommand : ICommand, IWebRequest
{
    public long ProvinceId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public long Priority { get; set; }
    public string CountryCoreId { get; set; } = string.Empty;

    public UpdateProvinceParameter ToParameter() => new(Title,
                                                       DisplayTitle,
                                                       Code,
                                                       Priority,
                                                       CountryCoreId);

    public string Path => "/Api/Province/UpdateProvince";
}
