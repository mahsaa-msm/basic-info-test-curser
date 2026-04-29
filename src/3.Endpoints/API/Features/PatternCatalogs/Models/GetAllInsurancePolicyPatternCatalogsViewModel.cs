using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.GetAll;
using Zamin.Core.RequestResponse.Endpoints;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Endpoints.API.Features.PatternCatalogs.Models;

public sealed class GetAllInsurancePolicyPatternCatalogsViewModel : IWebRequest
{
    public bool? IsActive { get; set; }
    public string Path => "/Api/PatternCatalog/GetAllPatternCatalogs";

    public GetAllPatternCatalogsQuery ToQuery() => new GetAllPatternCatalogsQuery
    {
        IsActive = IsActive,
        Type = PatternCatalogType.InsurancePolicyNumber
    };
}

