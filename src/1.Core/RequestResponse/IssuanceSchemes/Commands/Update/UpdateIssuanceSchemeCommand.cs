using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.IssuanceSchemes.Parameters;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.RequestResponse.IssuanceSchemes.Commands.Update;

public sealed class UpdateIssuanceSchemeCommand : ICommand, IWebRequest
{
    public long IssuanceSchemeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public DateTime? FromStartDateUtc { get;  set; }
    public DateTime? ToStartDateUtc { get;  set; }
    public DateTime? FromIssueDateUtc { get;  set; }
    public DateTime? ToIssueDateUtc { get;  set; }
    public string InsuranceTypeCoreId { get;  set; } = string.Empty;
    public AdjustmentType? AdjustmentType { get; set; }
    public double? AdjustmentPercent { get; set; }
    public long Priority { get; set; }

    public UpdateIssuanceSchemeParameter ToParameter() => new(Title,
                                                              DisplayTitle,
                                                              Code,
                                                              FromStartDateUtc,
                                                              ToStartDateUtc,
                                                              FromIssueDateUtc,
                                                              ToIssueDateUtc,
                                                              InsuranceTypeCoreId,
                                                              AdjustmentType,
                                                              AdjustmentPercent,
                                                              Priority);

    public string Path => "/Api/IssuanceScheme/UpdateIssuanceScheme";
}
