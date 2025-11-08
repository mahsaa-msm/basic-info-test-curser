using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.Provinces.Commands.Delete;
public sealed class DeleteProvinceCommand : ICommand, IWebRequest
{
    public long ProvinceId { get; set; }

    public string Path => "/Api/Province/DeleteProvince";
}