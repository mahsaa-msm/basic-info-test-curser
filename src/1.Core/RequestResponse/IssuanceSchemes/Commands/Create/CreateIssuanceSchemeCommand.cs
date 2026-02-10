using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.IssuanceSchemes.Parameters;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.RequestResponse.IssuanceSchemes.Commands.Create;

public sealed class CreateIssuanceSchemeCommand : ICommand<long>, IWebRequest
{
    public string Title { get; set; } = string.Empty;
    public string? DisplayTitle { get; set; }
    public string Code { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public DateTime? FromStartDateUtc { get; set; }
    public DateTime? ToStartDateUtc { get; set; }
    public DateTime? FromIssueDateUtc { get; set; }
    public DateTime? ToIssueDateUtc { get; set; }
    public string InsuranceTypeCoreId { get; set; } = string.Empty;
    public AdjustmentType? AdjustmentType { get; set; }
    public double? AdjustmentPercent { get; set; }
    public CreateIssuanceSchemeParameter ToCreateParameter(long priority) => new(Title,
                                                                                                        DisplayTitle,
                                                                                                        CoreId,
                                                                                                        Code,
                                                                                                        FromStartDateUtc,
                                                                                                        ToStartDateUtc,
                                                                                                        FromIssueDateUtc,
                                                                                                        ToIssueDateUtc,
                                                                                                        InsuranceTypeCoreId,
                                                                                                        AdjustmentType,
                                                                                                        AdjustmentPercent,
                                                                                                        IsActive.True(),
                                                                                                        priority);

    public string Path => "/Api/IssuanceScheme/CreateIssuanceScheme";
}