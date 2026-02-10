using Master.Data.Core.Domain.Common.ValueObjects;

namespace Master.Data.Core.Domain.InsuranceTypes.Parameters;

public sealed record RestoreInsuranceTypeParameter(DIPTitle Title,
                                                   NullableTitle DisplayTitle,
                                                   Code Code,
                                                   Common.ValueObjects.Priority Priority);
