using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Queries.GetById;

public sealed class GetLicensePlateTypeByIdQuery : IQuery<LicensePlateTypeQr?>, IWebRequest
{
    public long LicensePlateTypeId { get; set; }

    public string Path => "/Api/LicensePlateType/GetLicensePlateTypeById";
}
