using Master.Data.Core.Domain.Common.ValueObjects;

namespace Master.Data.Core.Domain.InsuranceTypes.Parameters;

public sealed record CreateInsuranceTypeParameter(DIPTitle Title,
                                                  NullableTitle DisplayTitle,
                                                  CoreId CoreId,
                                                  Code Code,
                                                  Common.ValueObjects.Priority Priority);