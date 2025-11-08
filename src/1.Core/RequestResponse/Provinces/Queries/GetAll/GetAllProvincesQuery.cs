using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.Provinces.Queries.GetAll;
public sealed class GetAllProvincesQuery : IQuery<List<ProvinceSelectItemQr>>, IWebRequest
{
    public bool? IsActive { get; set; }

    public string Path => "/Api/Province/GetAllProvinces";
}
