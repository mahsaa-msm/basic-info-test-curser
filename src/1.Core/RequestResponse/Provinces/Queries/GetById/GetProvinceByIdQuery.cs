using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.Provinces.Queries.GetById;

public sealed class GetProvinceByIdQuery : IQuery<ProvinceQr>, IWebRequest
{
    public long ProvinceId { get; set; }

    public string Path => "/Api/Province/GetProvinceById";
}
