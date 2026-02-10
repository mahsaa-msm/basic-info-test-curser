using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.InsuranceTypes.Queries.GetAll;

public sealed class GetAllInsuranceTypeQuery : IQuery<List<InsuranceTypeSelectItemQr>>, IWebRequest
{
    public bool? IsActive { get; set; }

    public string Path => "/Api/InsuranceType/GetAllInsuranceTypes";
}