using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.IssuanceScheme.GetAll;
public sealed class GetAllIssuanceSchemesResponse
{
    public int tarhSodoorID { get; set; }
    public int noeBimehID { get; set; }
    public string naam { get; set; } = string.Empty;    
    public string? code { get; set; }
    public bool faal { get; set; }
    public long? azTarikhSodoor { get; set; }
    public long? taTarikhSodoor { get; set; }
    public long? tarikhShorooAz { get; set; }
    public long? tarikhShorooTa { get; set; }
    public AdjustmentType takhfifEzafeh { get; set; }
    public int darsadTakhfifEzafeh { get; set; }
}
