using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.InsuranceUnits.Queries.GetById;
public sealed class GetInsuranceUnitByIdQuery : IQuery<InsuranceUnitQr>, IWebRequest
{
    public long InsuranceUnitId { get; set; }

    public string Path => "/Api/InsuranceUnit/GetInsuranceUnitById";
}