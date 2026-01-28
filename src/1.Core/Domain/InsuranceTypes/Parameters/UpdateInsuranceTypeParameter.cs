using Master.Data.Core.Domain.Common.ValueObjects;

namespace Master.Data.Core.Domain.InsuranceTypes.Parameters;

public sealed record UpdateInsuranceTypeParameter(DIPTitle Title,
                                                  DIPTitle DisplayTitle,
                                                  Code Code,
                                                  Common.ValueObjects.Priority Priority);