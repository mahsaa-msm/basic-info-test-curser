using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.InsuranceTypes.Queries.GetById;

public sealed class GetInsuranceTypeByIdQuery : IQuery<InsuranceTypeQr>, IWebRequest
{
    public long InsuranceTypeId { get; set; }

    public string Path => "/Api/InsuranceType/GetInsuranceTypeById";
}
